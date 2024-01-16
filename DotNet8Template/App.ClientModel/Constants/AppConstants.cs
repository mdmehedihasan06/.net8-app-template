using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ClientModel.Constants
{
    public class AppConstants
    {
        public enum StatusId
        {
            Pending = 0,
            Active = 1,
            Inactive = 2
        }
        public enum ApprovalStatusId
        {
            Pending = 0,
            PartiallyApproved = 1,
            Approved = 2,
            UnderReview = 3,
            Rejected = 4
        }
    }
}
