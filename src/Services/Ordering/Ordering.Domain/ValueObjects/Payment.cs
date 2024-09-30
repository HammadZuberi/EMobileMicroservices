﻿

namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
		public string? CardName { get; } = default!;
		public string CardNumber { get; } = default!;
		public string Expiration { get; } = default!;
		public string CVV { get; } = default!;
		public int PaymentMethod { get; } = default!;

		protected Payment()
		{

		}

		private Payment(string? cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
		{
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CVV = cVV;
			PaymentMethod = paymentMethod;
		}

		public Payment of(string? cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
			ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
			ArgumentException.ThrowIfNullOrWhiteSpace(CVV);
			ArgumentOutOfRangeException.ThrowIfNotEqual(CVV.Length,3);//check CVV lenght

			return new Payment(cardName, cardNumber, expiration, cVV, paymentMethod);
		}
	}
}
