
namespace EmployeeContract.Reports;

public interface IReportDocumentWithContextTextsContract : IReportDocumentContract
{
    /// <summary> 
    /// Создание документа в асинхронном режиме 
    /// </summary> 
    /// <param name="filePath">Путь до файла</param> 
    /// <param name="header">Заголовок документа</param> 
    /// <param name="paragraphs">Список абзацев текста</param> 
    /// <exception cref="ArgumentNullException">Не указан путь до  файла</exception>
    /// <exception cref="ArgumentNullException">Не задан заголовок документа</exception> 
    /// <exception cref="ArgumentNullException">Список абзацев текста не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Передан пустой список абзацев текста</exception>
    /// <exception cref="ArgumentNullException">В списке абзацев 
    /// текста имеется абзац с не заданной строкой</exception> 
    /// <returns>Задача по созданию документа</returns> 
    Task CreateDocumentAsync(
     string filePath,
     string header,
     List<string> paragraphs
    );
}
