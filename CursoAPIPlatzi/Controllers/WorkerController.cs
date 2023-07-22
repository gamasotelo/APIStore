using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPIPlatzi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;
        public WorkerController(IWorkerService workerService) {
            _workerService = workerService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var workersDTO = _workerService.GetAll();
            return Ok(workersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var workerDTO = _workerService.GetById(id);
            return workerDTO == null ? NotFound() : Ok(workerDTO);
        }

        [HttpPost]
        public IActionResult Create(Worker worker) { 
            var createdWorker = _workerService.Create(worker);
            return createdWorker == null ? NotFound() : CreatedAtAction(nameof(GetById), new { createdWorker.Id }, createdWorker);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Worker newWorker)
        {
            var workerUpdated = _workerService.Update(id,newWorker);
            return workerUpdated == null ? BadRequest() : Ok(workerUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var worker = _workerService.Delete(id);
            return worker == null ? BadRequest() : Ok(worker);
        }
    }
}
