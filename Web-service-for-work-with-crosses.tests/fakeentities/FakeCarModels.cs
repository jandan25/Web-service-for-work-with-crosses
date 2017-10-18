using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_service_for_work_with_crosses.tests.fakeentities
{
    public partial class FakeCarModels
    {
        [Key]
        public int FakeCarModelID { get; set; }

        public int FakeManufactorID { get; set; }

        public string FakeName { get; set; }
    }
}
