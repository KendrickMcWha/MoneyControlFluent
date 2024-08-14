namespace MoneyControl.Domain.Records;

public record TransactionParamPayload(int accountId, int categoryId, DateOnly? startDate, DateOnly? endDate);
