using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_API.Controllers;
using Rest_API.Models;
using Rest_API.Services.CharacterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API.Controllers.Tests
{
    [TestClass()]
    public class CharacterControllerTests
    {
        [TestMethod()]
        public void Test_PingMethod()
        {
            var controller = new CharacterController(null);

            var result = controller.Ping() as ActionResult<string>;

            Assert.IsNotNull(result);
            Assert.AreEqual("Dogshouseservice.Version1.0.1", result.Value);
        }
        [TestMethod()]
        public async Task Test_AddDogsSameNameMethodAsync()
        {
            var characterService = new CharacterService();
            var controller = new CharacterController(characterService);
            var existingCharacter = new Character { Name = "Oleg" };
            var newCharacter = new Character { Name = "Oleg" };

            var result = await controller.AddDogs(newCharacter);

            Assert.AreEqual(200, ((ObjectResult)result.Result).StatusCode);

        }
        [TestMethod()]
        public async Task Test_AddDogsNegativeTailLengthMethodAsync()
        {
            var characterService = new CharacterService();
            var controller = new CharacterController(characterService);
            var newCharacter = new Character { Name = "Buddy", Tail_length = -5 };

            var result = await controller.AddDogs(newCharacter);

            Assert.AreEqual(400, ((ObjectResult)result.Result).StatusCode);

        }
        [TestMethod]
        public async Task Get_ValidAttributes_ReturnsOkResult()
        {
            var controller = new CharacterController(new CharacterService());

            var result = await controller.GetDogs("name", "asc", 1, 10);

            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Character>>));
        }

        [TestMethod]
        public async Task Get_InvalidAttribute_ReturnsBadRequest()
        {
            var controller = new CharacterController(new CharacterService());

            var result = await controller.GetDogs("invalid", "asc", 1, 10);

            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Character>>));
        }

        [TestMethod]
        public async Task Get_ValidAttributes_PagedResults_ReturnsOkResult()
        {
            var controller = new CharacterController(new CharacterService());

            var result = await controller.GetDogs("name", "asc", 1, 10);

            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Character>>));
        }


    }
}