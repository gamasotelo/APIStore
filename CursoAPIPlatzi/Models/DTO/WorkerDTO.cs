using CursoAPIPlatzi.Models.Domain;

namespace CursoAPIPlatzi.Models.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Position WorkPosition { get; set; }
    }
}
