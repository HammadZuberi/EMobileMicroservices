
namespace Ordering.Domain.ValueObjects
{

	//in terms of defining value objects for immutablity
	public record Address
	{
		public string FirstName { get; } = default!;
		public string LastName { get; } = default!;
		public string? EmailAddress { get; } = default!;
		public string AddressLine { get; } = default!;
		public string Country { get; } = default!;
		public string State { get; } = default!;
		public string ZipCode { get; } = default!;

		//default for core
		protected Address() { }

		private Address(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipCode)
		{
			FirstName = firstName;
			LastName = lastName;
			EmailAddress = emailAddress;
			AddressLine = addressLine;
			Country = country;
			State = state;
			ZipCode = zipCode;
		}
		public Address of(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipCode)
		{

			ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
			ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);


			return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);

		}
	}
}
