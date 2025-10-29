using EmployeeContract.Reports;

public interface IReportDocumentWithTableRowHeaderContract :
IReportDocumentContract
{
    /// <summary> 
    /// Создание документа в асинхронном режиме 
    /// </summary> 
    /// <typeparam name="T"></typeparam> 
    /// <param name="filePath">Путь до файла</param> 
    /// <param name="header">Заголовок документа</param> 
    /// <param name="columnsWidth">Ширина колонок</param> 
    /// <param name="columnUnions">Список колонок заголовка для объединения 
    /// (индекс первой колонки, с которой начинается объединенние и количество
    /// объединяемых колонок)</param> 
    /// <param name="headers">Список заголовков (указывается индекс ячейки, 
    /// в которую нужно вставить заголовок, а также название свойства или поля, из которого
    /// следует извлекать значение из элемента данных для заполнения ячейки строки)</param> 
    /// <param name="data">Данные для заполнения таблицы</param> 
    /// <exception cref="ArgumentNullException">Не указан путь до файла</exception>
    /// <exception cref="ArgumentNullException">Не задан заголовок документа</exception> 
    /// <exception cref="ArgumentNullException">Список columnsWidth не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Список columnsWidth пустой</exception> 
    /// <exception cref="ArgumentNullException">Список columnUnions не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Список columnUnions пустой</exception> 
    /// <exception cref="ArgumentNullException">Список заголовков не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Список заголовков пустой</exception> 
    /// <exception cref="ArgumentException">Список columnsWidth не совпадает 
    /// по количеству заголовков второй строки из списка заголовков</exception>
    /// <exception cref="ArgumentException">Имеется несоответсвие списка 
    /// объединения колонок и списка заголовков</exception> 
    /// <exception cref="ArgumentNullException">Список данных для заполнения не задан</exception>
    /// <exception cref="ArgumentOutOfRangeException">Список данных для заполнения пустой</exception>
    /// <exception cref="ArgumentException">Список данных для заполнения не 
    /// совпадает по количеству заголовков второй строки из списка заголовков</exception> 
    /// <returns>Задача по созданию документа</returns> 
    Task CreateDocumentAsync<T>(
      string filePath,
      string header,
      List<int> columnsWidth,
      List<(int StartIndex, int Count)> columnUnions,
      List<(int ColumnIndex, int RowIndex, string Header, string PropertyName, string FiledName)> headers,
      List<T> data
   );
}