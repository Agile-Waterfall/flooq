using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flooq.Api.Controllers;
using Flooq.Api.Metrics.Services;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flooq.Test.Controllers;

[TestClass]
public class TokenControllerTest
{
  private static readonly Guid TestUserId = Guid.NewGuid();
  
  private readonly Token _token = new() 
  {
    Id = Guid.NewGuid(),
    Name = "Demo Token #1",
    UserId = TestUserId,
    LastEdited = DateTime.Now,
    Value = "TestToken"
  };
  private readonly ClaimsPrincipal _user = new(new ClaimsIdentity(new[]
  {
    new Claim(ClaimTypes.NameIdentifier, TestUserId.ToString())
  }, "mock"));
  
  private readonly Mock<ITokenService> _tokenServiceMock = new();
  private readonly Mock<ITokenMetricsService> _tokenMetricsServiceMock = new();


  [TestMethod]
  public void CanCreateTokenController()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    
    Assert.IsNotNull(tokenController);
  }
  
  [TestMethod]
  public async Task CanGetTokenIdsAndNamesByUser_Zero()
  {
    var tokenNames = new List<Dictionary<string, string>>(); 
    var actionResult = new ActionResult<IEnumerable<Dictionary<string, string>>>(tokenNames);
    _tokenServiceMock.Setup(service => service.GetTokenIdsAndNamesByUserId(TestUserId)).ReturnsAsync(actionResult);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
    Assert.IsNull(actionResultReceived.Value?.GetEnumerator().Current);
  }

  [TestMethod]
  public async Task CanGetTokenIdsAndNamesByUser_One()
  {
    var tokenIdNameDict = new Dictionary<string, string>
    {
      {"Id", _token.Id.ToString()!},
      {"Name", _token.Name!}
    };
    var tokenNamesAndIds = new List<Dictionary<string, string>>() {tokenIdNameDict};
    var actionResult = new ActionResult<IEnumerable<Dictionary<string, string>>>(tokenNamesAndIds);
    _tokenServiceMock.Setup(service => service.GetTokenIdsAndNamesByUserId(TestUserId)).ReturnsAsync(actionResult);
    
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetTokenIdsAndNamesByUser_Multiple()
  {
    var token1IdNameDict = new Dictionary<string, string>
    {
      {"Id", _token.Id.ToString()!},
      {"Name", _token.Name!}
    };
    var token2IdNameDict = new Dictionary<string, string>
    {
      {"Id", Guid.NewGuid().ToString()},
      {"Name", "Demo Token # 2"}
    };
    var tokenNamesAndIds = new List<Dictionary<string, string>> {token1IdNameDict, token2IdNameDict};
    var actionResult = new ActionResult<IEnumerable<Dictionary<string, string>>>(tokenNamesAndIds);
    _tokenServiceMock.Setup(service => service.GetTokenIdsAndNamesByUserId(TestUserId)).ReturnsAsync(actionResult);
    
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResultReceived = await tokenController.GetTokenNamesByUser();
    
    Assert.AreSame(actionResult, actionResultReceived);
  }

  [TestMethod]
  public async Task CanGetToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);

    var actionResultReceived = await tokenController.GetToken(_token.Id);
    var tokenReceived = actionResultReceived.Value;
    
    Assert.AreSame(_token, tokenReceived);
  }

  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingToken()
  {
    var newId = Guid.NewGuid();
    _tokenServiceMock.Setup(service => service.GetTokenById(newId)).ReturnsAsync(new ActionResult<Token?>((Token?) null));
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);

    var actionResult = await tokenController.GetToken(newId);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPutToken()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(true);
    _tokenServiceMock.Setup(service => service.PutToken(_token)).Returns(new ActionResult<Token>(_token));
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };
    
    var actionResult = await tokenController.PutToken(_token.Id, _token);
    
    Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Token>));
    Assert.AreSame(_token, actionResult.Value);
  }

  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdsAreNotEqual()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(Guid.NewGuid(), _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }
  
  [TestMethod]
  public async Task Put_ReturnsBadRequestIfIdIsNull()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(null, _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
  }

  [TestMethod]
  public async Task Put_ReturnsUnauthorizedIfTokenIsNotOwnedByUser()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(false);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PutToken(_token.Id, _token);

    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult.Result, typeof(UnauthorizedResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateConcurrencyException))]
  public async Task Put_ThrowsExceptionIfAMatchingTokenExistsButCouldNotBeOverriden()
  {
    _tokenServiceMock.Setup(service => service.IsTokenOwnedByUser(_token.Id, _token.UserId)).Returns(true);
    _tokenServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateConcurrencyException());
    _tokenServiceMock.Setup(service => service.TokenExists(_token.Id)).Returns(true);
    
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    await tokenController.PutToken(_token.Id, _token);
  }


  [TestMethod]
  public async Task CanPostToken()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.PostToken(_token);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = actionResult.Result as CreatedAtActionResult;

    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_token, createdAtAction.Value);
    Assert.AreEqual(_token.UserId, TestUserId);
  }

  [TestMethod]
  public async Task Post_ReturnsConflictIfTokenAlreadyExists()
  {
    _tokenServiceMock.Setup(service => service.TokenExists(_token.Id)).Returns(true);
    _tokenServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new ArgumentException());
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object)
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = new DefaultHttpContext { User = _user }
      }
    };

    var actionResult = await tokenController.PostToken(_token);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(ConflictResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateException))]
  public async Task Post_ThrowsExceptionIfTokenDoesNotExistButCannotBePosted() 
  {
    _tokenServiceMock.Setup(service => service.SaveChangesAsync()).ThrowsAsync(new DbUpdateException());
    _tokenServiceMock.Setup(service => service.TokenExists(_token.Id)).Returns(false);
    
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    await tokenController.PostToken(_token);
  }
  
  [TestMethod]
  public async Task Post_ReturnsConflictIfUserHasEquallyNamedToken()
  {
    _tokenServiceMock.Setup(service => service.HasUserEquallyNamedToken(_token.UserId, _token.Name!)).Returns(true);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object)
    {
      ControllerContext = new ControllerContext
      {
        HttpContext = new DefaultHttpContext { User = _user }
      }
    };

    var actionResult = await tokenController.PostToken(_token);
    
    Assert.IsInstanceOfType(actionResult.Result, typeof(ConflictResult));
  }

  [TestMethod]
  public async Task CanDeleteToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);

    var actionResult = await tokenController.DeleteToken(_token.Id);
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Delete_ReturnsNotFoundIfThereIsNoMatchingToken()
  {
    _tokenServiceMock.Setup(service => service.GetTokenById(_token.Id)).ReturnsAsync(_token);
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);

    var actionResult = await tokenController.DeleteToken(Guid.NewGuid());
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanDeleteAllTokens()
  {
    var tokenController = new TokenController(_tokenServiceMock.Object, _tokenMetricsServiceMock.Object);
    tokenController.ControllerContext = new ControllerContext
    {
      HttpContext = new DefaultHttpContext { User = _user }
    };

    var actionResult = await tokenController.DeleteAllTokens();
    
    Assert.IsNotNull(actionResult);
    Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
  }
}