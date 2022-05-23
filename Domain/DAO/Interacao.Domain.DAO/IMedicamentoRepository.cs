using Interacao.Entity;

namespace Interacao.Domain.DAO
{
    public interface IMedicamentoRepository
    {
        Task AdicionarNovoMedicamento(Medicamento entity);
        public Task<List<Medicamento>> ObterTodosOsMedicamentos();
    }
}