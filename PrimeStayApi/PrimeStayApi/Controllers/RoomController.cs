﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Services;
using System.Collections.Generic;

namespace PrimeStayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IDao<RoomEntity> _dao;
        public RoomController(IDao<RoomEntity> dao)
        {
            _dao = dao;
        }

        // GET: RoomController
        [HttpGet]
        public IEnumerable<RoomEntity> Index(int? id, string type, int? num_of_available, int? num_of_beds, string description, int? rating, int? hotel_id) => _dao.ReadAll(new RoomEntity()
        {
            Id = id,
            Type = type,
            Num_of_avaliable = num_of_available,
            Num_of_beds = num_of_beds,
            Description = description,
            Rating = rating,
            Hotel_Id = hotel_id
        });

        [HttpGet]
        [Route("{id}")]
        public RoomEntity Details(int id) => _dao.ReadById(id);

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            IntParser parser = new();
            int availableRooms = parser.parseInt(collection["numOfAvailableRooms"]);
            int availableBeds = parser.parseInt(collection["numOfAvailableBeds"]);
            int hotelId = parser.parseInt(collection["hotelId"]);
            int rating = parser.parseInt(collection["rating"]);


            RoomEntity room = new()
            {
                Type = collection["type"],
                Description = collection["description"],
                Num_of_avaliable = availableRooms,
                Num_of_beds = availableBeds,
                Hotel_Id = hotelId,
                Rating = rating
            };

            int id = _dao.Create(room);

            return Created(id.ToString(), room);
        }


        [HttpPut]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            IntParser parser = new();
            int availableRooms = parser.parseInt(collection["numOfAvailableRooms"]);
            int availableBeds = parser.parseInt(collection["numOfAvailableBeds"]);
            int hotelId = parser.parseInt(collection["hotelId"]);
            int rating = parser.parseInt(collection["rating"]);


            RoomEntity room = new()
            {
                Id = id,
                Type = collection["type"],
                Description = collection["description"],
                Num_of_avaliable = availableRooms,
                Num_of_beds = availableBeds,
                Hotel_Id = hotelId,
                Rating = rating
            };

            return _dao.Update(room) == 1 ? Ok() : NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            RoomEntity room = new()
            {
                Id = id,
            };

            return _dao.Delete(room) == 1 ? Ok() : NotFound();
        }
    }
}
