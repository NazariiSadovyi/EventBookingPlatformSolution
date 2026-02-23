using EBP.Domain.Enums;

namespace EBP.Domain.ValueObjects
{
    public class TicketType
    {
        public TicketKind Kind { get; private set; }
        public decimal Price { get; private set; }

        private TicketType() { }

        public override bool Equals(object? obj)
        {
            if (obj is not TicketType other)
                return false;

            return Kind == other.Kind && Price == other.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Kind, Price);
        }

        public static TicketType Create(TicketKind kind, decimal price)
        {
            return new TicketType
            {
                Kind = kind,
                Price = price
            };
        }
    }
}
