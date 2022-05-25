using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Flooq.Api.Models;
using IdentityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NuGet.Protocol;
using FlooqWebApplicationFactory = Flooq.IntegrationTest.FlooqWebApplicationFactory<Program>;

namespace Flooq.IntegrationTest;

[TestClass]
public class ContactTest
{
  private readonly HttpClient _client = FlooqWebApplicationFactory.Factory.CreateClient();

  [TestMethod]
  public async Task CanGetContacts()
  {
    _client.DefaultRequestHeaders.Authorization 
      = new AuthenticationHeaderValue("Bearer", MockJwtTokens.GenerateJwtToken(new List<Claim>()
      {
        new (JwtClaimTypes.Scope, "read_all"),
        new (ClaimTypes.NameIdentifier, FlooqWebApplicationFactory.TestUserId.ToString())
      }));
    var response = await _client.GetAsync("api/Contact");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var contacts = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, string>>>(content)!;
    
    Assert.IsFalse(contacts.ToImmutableList().IsEmpty);
  }

  [TestMethod]
  public async Task CanGetContact()
  {
    var response = await _client.GetAsync($"api/Contact/{FlooqWebApplicationFactory.TestEmail}");
    response.EnsureSuccessStatusCode();
    
    var content = response.Content.ReadAsStringAsync().Result;
    var contact = JsonConvert.DeserializeObject<Contact>(content)!;
    
    Assert.AreEqual(FlooqWebApplicationFactory.TestEmail, contact.Email);
  }

  [TestMethod]
  public async Task CannotGetNonExistingContact()
  {
    var response = await _client.GetAsync($"api/Contact/not@flooq.io");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CanPostContact()
  {
    var contact = new Contact("new@flooq.io");
    var content = new StringContent(contact.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Contact", content);
    response.EnsureSuccessStatusCode();
    
    var receivedContent = response.Content.ReadAsStringAsync().Result;
    var receivedContact = JsonConvert.DeserializeObject<Contact>(receivedContent)!;
    
    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    Assert.AreEqual(contact.Email, receivedContact.Email);
  }

  [TestMethod]
  public async Task CannotPostExistingContact()
  {
    var contact = new Contact(FlooqWebApplicationFactory.TestEmail);
    var content = new StringContent(contact.ToJson(), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("api/Contact", content);
    
    Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
  }

  [TestMethod]
  public async Task CanDeleteContact()
  {
    var response = await _client.DeleteAsync($"api/Contact/{FlooqWebApplicationFactory.TestEmail}");
    response.EnsureSuccessStatusCode();
    
    Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

    response = await _client.GetAsync($"api/Contact/{FlooqWebApplicationFactory.TestEmail}");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [TestMethod]
  public async Task CannotDeleteNonExistingContact()
  {
    var response = await _client.DeleteAsync($"api/Contact/not@flooq.io");
    
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }
}