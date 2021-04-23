using Microsoft.AspNetCore.Mvc;
using Mk_Api.Data.Collections;
using Mk_Api.Models;
using MongoDB.Driver;

namespace Mk_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FighterController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Fighter> _fightersCollection;

        public FighterController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _fightersCollection = _mongoDB.DB.GetCollection<Fighter>(typeof(Fighter).Name.ToLower());
        }

        [HttpPost]
        public IActionResult SaveFighter([FromBody] FighterDto dto)
        {
            var fighter = new Fighter(dto.Name, dto.Class, dto.Birthplace, dto.Age);
            _fightersCollection.InsertOne(fighter);
            return StatusCode(201, "Lutador adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult GetFighters(){
            var fighters = _fightersCollection.Find(Builders<Fighter>.Filter.Empty).ToList();
            return Ok(fighters);
        }
        
        [HttpDelete]
        public ActionResult DeleteFighter(string name)
        {
           var filter = Builders<Fighter>.Filter.Where(f => f.Name == name);
           _fightersCollection.DeleteOne(filter);

           return StatusCode(201, "Deletado com sucesso"); 
        }

    }
}