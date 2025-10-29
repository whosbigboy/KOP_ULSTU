
namespace EmployeeContract.Reports;

public interface IReportDocumentWithChartLineContract : IReportDocumentContract
{
    /// <summary> 
    /// Создание документа в асинхронном режиме 
    /// </summary> 
    /// <param name="filePath">Путь до файла</param> 
    /// <param name="header">Заголовок документа</param> 
    /// <param name="chartTitle">Заголовок диаграммы</param> 
    /// <param name="series">Список серий с данными для линейной диаграммы</param>
    /// <exception cref="ArgumentNullException">Не указан путь до файла</exception> 
    /// <exception cref="ArgumentNullException">Не задан заголовок документа</exception>
    /// <exception cref="ArgumentNullException">Не задан заголовок диаграммы</exception> 
    /// <exception cref="ArgumentNullException">Словарь серий не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Словарь серий пустой</exception> 
    /// <exception cref="ArgumentNullException">Список серии не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Список серии пустой</exception> 
    /// <exception cref="ArgumentException">В разных сериях различаются параметры</exception>
    /// <returns>Задача по созданию документа</returns> 
    Task CreateDocumentAsync(
         string filePath,
         string header,
         string chartTitle,
         Dictionary<string, List<(int Parameter, double Value)>> series
    );
}
