using System;
namespace SimpleCrm
{
	public class CustomerListParameters
	{
		public int Page { get; set; } = 1; // 1 based page number

		public int Take { get; set; } = 50; // Page size, or number of records to take in a page

		public string OrderBy { get; set; } // Any valid order specification over customer properties

		public string LastName { get; set; } // If specified, the exact lastname value to match (not a partial match)

		public string Term { get; set; } // A common term to search among all 'searchable' fields. This is a partial value to find within any of those searchable fields
    }
}

