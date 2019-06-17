using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace webConnect.Controllers
{
    public class SwAPIController : AbstractController{

        private static List<Character> _characters = new List<Character>();
        
        // [HttpPost("Characters")]
        // public bool addCharacter([FromBody] Character character){
        //     _characters.Add(character);
        //     return true;
        // }
        [HttpPost("Characters")]
        public string addCharacter([FromBody] List<Character> characters){
            _characters = characters;

            try{
                //DBWrapper.sendData(characters);
            }catch(Exception e){
                return (e.Message == "") ? "vsv" : e.Message;
            } 
            return "eg";
        }

        [HttpGet("Characters/{name}")]
        public Character getCharacter(string name){
            // foreach(var character in _characters){
            //     if(character.Name == name){
            //         return character;
            //     }
            // }
            Character character;
            try{
                character = DBWrapper.getCharacterData(name);
            }catch(Exception e){
                return new Character {
                Name = e.Message,
                Height = "Not Available",
                Gender = "Not Available"
                };
            }
            
            return (character != null) ? character : 
            new Character {
                Name = "Not Available",
                Height = "Not Available",
                Gender = "Not Available"
            };
        }

        [HttpDelete("Characters/{name}")]
        public Character removeCharacter(string name){
            foreach(Character character in _characters){
                if(character.Name == name){
                    _characters.Remove(character);
                    return character;
                }
            }
            return null;
        }

        [HttpGet("Characters")]
        public IEnumerable<Character> getCharacters(){
            return _characters;
        }

        public class Character{
            
            public string Name { get; set; }
            public string Height { get; set; }
            public string Gender { get; set; }
        }
    }

    

}
