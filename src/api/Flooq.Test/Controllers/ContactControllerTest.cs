using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flooq.Api.Controllers;
using Flooq.Api.Metrics.Services;
using Flooq.Api.Models;
using Flooq.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Flooq.Test.Controllers;

[TestClass]
public class ContactControllerTest
{
  private readonly Mock<IContactService> _contactServiceMock = new();
  private readonly Mock<IContactMetricsService> _contactMetricsServiceMock = new();
  private readonly Contact _contact = new("test@example.com");
  private readonly Random _random = new();
  private int _n;

  [TestInitialize]
  public void Setup()
  {
    _n = _random.Next(2, 11);
  }

  [TestMethod]
  public void CanCreateContactController()
  {
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);
    Assert.IsNotNull(contactController);
  }

  [TestMethod]
  public async Task CanGetContacts_Zero()
  {
    var contacts = new List<Contact>();
    var contactActionResult = new ActionResult<IEnumerable<Contact>>(contacts);
    _contactServiceMock.Setup(service => service.GetContacts()).ReturnsAsync(contactActionResult);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.GetContacts();
    Assert.AreSame(contactActionResult, receivedActionResult);
    Assert.AreEqual(0, receivedActionResult.Value?.Count());
  }

  [TestMethod]
  public async Task CanGetContacts_One()
  {
    var contacts = new List<Contact>{_contact};
    var contactActionResult = new ActionResult<IEnumerable<Contact>>(contacts);
    _contactServiceMock.Setup(service => service.GetContacts()).ReturnsAsync(contactActionResult);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.GetContacts();
    Assert.AreSame(contactActionResult, receivedActionResult);
    Assert.AreEqual(1, receivedActionResult.Value?.Count());
  }

  [TestMethod]
  public async Task CanGetContacts_Multiple()
  {
    var contacts = new List<Contact>();
    for (var i = 0; i < _n; i++)
    {
      contacts.Add(new Contact(i + "@example.com"));
    }

    var contactActionResult = new ActionResult<IEnumerable<Contact>>(contacts);
    _contactServiceMock.Setup(service => service.GetContacts()).ReturnsAsync(contactActionResult);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.GetContacts();
    Assert.AreSame(contactActionResult, receivedActionResult);
    Assert.AreEqual(_n, receivedActionResult.Value?.Count());
  }

  [TestMethod]
  public async Task CanGetContact()
  {
    _contactServiceMock.Setup(service => service.GetContact(_contact.Email)).ReturnsAsync(_contact);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.GetContact(_contact.Email);
    var receivedContact = receivedActionResult.Value;
    Assert.AreSame(_contact, receivedContact);
  }
  
  [TestMethod]
  public async Task Get_ReturnsNotFoundIfThereIsNoMatchingContact()
  {
    const string newEmail = "not@existing.com";
    
    _contactServiceMock.Setup(service => service.GetContact(_contact.Email)).ReturnsAsync(new ActionResult<Contact?>(_contact));
    _contactServiceMock.Setup(service => service.GetContact(newEmail)).ReturnsAsync(new ActionResult<Contact?>((Contact?) null));
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.GetContact(_contact.Email);
    Assert.IsInstanceOfType(receivedActionResult, typeof(ActionResult<Contact>));
    
    var receivedContact = receivedActionResult.Value;
    Assert.IsNotNull(receivedContact);
    Assert.AreSame(_contact, receivedContact);

    receivedActionResult = await contactController.GetContact(newEmail);
    Assert.IsInstanceOfType(receivedActionResult, typeof(ActionResult<Contact>));
    
    receivedContact = receivedActionResult.Value;
    Assert.IsNull(receivedContact);
    Assert.IsInstanceOfType(receivedActionResult.Result, typeof(NotFoundResult));
  }

  [TestMethod]
  public async Task CanPostContact()
  {
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.PostContact(_contact);
    Assert.IsInstanceOfType(receivedActionResult.Result, typeof(CreatedAtActionResult));

    var createdAtAction = receivedActionResult.Result as CreatedAtActionResult;
    Assert.IsNotNull(createdAtAction?.Value);
    Assert.AreSame(_contact, createdAtAction.Value);
  }

  [TestMethod]
  public async Task Post_ReturnsConflictIfContactAlreadyExists()
  {
    _contactServiceMock.Setup(service => service.ContactExists(_contact.Email)).Returns(true);
    _contactServiceMock.Setup(service => service.SaveChangesAsync()).Throws(new DbUpdateException());
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.PostContact(_contact);
    Assert.IsInstanceOfType(receivedActionResult.Result, typeof(ConflictResult));
  }

  [TestMethod]
  [ExpectedException(typeof(DbUpdateException))]
  public async Task Post_ThrowsExceptionIfSaveChangesAsyncIsNotSuccessful()
  {
    _contactServiceMock.Setup(service => service.SaveChangesAsync()).Throws(new DbUpdateException());
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    await contactController.PostContact(_contact);
  }

  [TestMethod]
  public async Task CanDeleteContact()
  {
    _contactServiceMock.Setup(service => service.GetContact(_contact.Email)).ReturnsAsync(_contact);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    var receivedActionResult = await contactController.DeleteContact(_contact.Email);
    Assert.IsNotNull(receivedActionResult);
    Assert.IsInstanceOfType(receivedActionResult, typeof(NoContentResult));
  }

  [TestMethod]
  public async Task Delete_ReturnsNotFoundIfThereIsNoMatchingContact()
  {
    _contactServiceMock.Setup(service => service.GetContact(_contact.Email)).ReturnsAsync(_contact);
    var contactController = new ContactController(_contactServiceMock.Object, _contactMetricsServiceMock.Object);

    const string newEmail = "not@existing.com";
    var receivedActionResult = await contactController.DeleteContact(newEmail);
    
    Assert.IsNotNull(receivedActionResult);
    Assert.IsInstanceOfType(receivedActionResult, typeof(NotFoundResult));
  }
}