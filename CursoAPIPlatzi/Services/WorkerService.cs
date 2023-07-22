using CursoAPIPlatzi.Models.Domain;
using CursoAPIPlatzi.Models.DTO;

namespace CursoAPIPlatzi.Services
{
    public class WorkerService:IWorkerService
    {
        private readonly StoreContext _dbContext;
        public WorkerService(StoreContext dbContext) { 
            _dbContext = dbContext;
        }
        public IEnumerable<WorkerDTO> GetAll()
        {
            List<WorkerDTO> workerDTOs = new List<WorkerDTO>();
            var workers = _dbContext.Workers;
            foreach (var worker in workers) 
            {
                var workerDTO = _ConvertToDTO(worker);
                workerDTOs.Add(workerDTO);
            }
            return workerDTOs;
        }

        public WorkerDTO? GetById(int id) {
            var worker = _dbContext.Workers.FirstOrDefault(p => p.Id == id);
            if (worker == null) 
            { 
                return null; 
            }
            var workerDTO = _ConvertToDTO(worker);
            return workerDTO;
        }

        public WorkerDTO? Create(Worker worker) { 
            var newWorker = new Worker() 
            { 
                Id = worker.Id,
                Name = worker.Name,
                WorkPosition = worker.WorkPosition,
            };
            try
            {
                _dbContext.Workers.Add(newWorker);
                _dbContext.SaveChanges();
                var workerDTO = _ConvertToDTO(worker);
                return workerDTO;
            }catch (ArgumentException)
            {
                return null;
            }
        }

        public WorkerDTO? Update(int id, Worker newWorker) 
        {
            var oldWorker = _dbContext.Workers.FirstOrDefault(p => p.Id == id);
            if (oldWorker == null)
            { 
                return null; 
            }
            oldWorker.Name = newWorker.Name;
            oldWorker.WorkPosition = newWorker.WorkPosition;
            _dbContext.SaveChanges();
            var workerDTO = _ConvertToDTO(oldWorker);
            return workerDTO;
        }

        public WorkerDTO? Delete(int id)
        {
            var worker = _dbContext.Workers.FirstOrDefault(p => p.Id == id);
            if (worker == null) {
                return null;
            }

            _dbContext.Workers.Remove(worker);
            _dbContext.SaveChanges();
            var workerDTO = _ConvertToDTO(worker);
            return workerDTO;
        }

        private WorkerDTO _ConvertToDTO(Worker worker)
        {
            var dto = new WorkerDTO()
            {
                Id = worker.Id,
                Name = worker.Name,
                WorkPosition = worker.WorkPosition
            };
            return dto;
        }
    }

    

    public interface IWorkerService 
    {
        IEnumerable<WorkerDTO> GetAll();
        WorkerDTO? GetById(int id);
        WorkerDTO? Update(int id, Worker newWorker);
        WorkerDTO? Create(Worker worker);
        WorkerDTO? Delete(int id);
    }
}
