namespace B1TestTask2.Services.Dtos.Response
{
    public class RecordDto
    {
        public string BankAccount { get; set; }

        public decimal ActiveOpeningBalance { get; set; }

        public decimal PasiveOpeningBalance { get; set; }

        public decimal DebitTurnover { get; set; }

        public decimal CreditTurnover { get; set; }

        public decimal ActiveOutgoingBalance { get; set; }

        public decimal PassiveOutgoingBalance { get; set; }

        public string ClassId { get; set; }

        public string ClassName { get; set; }
    }
}
