﻿using System;
using System.Collections.Generic;
using Tu.Imports;
using Tu.Services;

namespace Tu.Washing
{
    public class WashingOutputColumns : List<OutputColumn>
    {
        readonly IRulesService _rulesService;

        public WashingOutputColumns()
            : this(new RulesServiceClient())
        {
        }

        public WashingOutputColumns(IRulesService rulesService)
        {
            _rulesService = rulesService;

            Add(new OutputColumn("GovID", typeof(string), "GovIDRule", _rulesService.IsValid) { IsKey = true });
            Add(new OutputColumn("DataProviderID", typeof(string)));
            Add(new OutputColumn("Title", typeof(string), "TitleRule", _rulesService.IsValid));
            Add(new OutputColumn("TitleDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Surname", typeof(string), "SurnameRule", _rulesService.IsValid));
            Add(new OutputColumn("SurnameDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("FirstNames", typeof(string), "ForenameRule", _rulesService.IsValid));
            Add(new OutputColumn("FirstNamesDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeTel1Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel1No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeTel2Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel2No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeTel3Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel3No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeTel4Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel4No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeTel5Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel5No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("HomeTel5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("WorkTel1Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel1No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("WorkTel2Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel2No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("WorkTel3Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel3No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("WorkTel4Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel4No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("WorkTel5Code", typeof(string), "TelCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel5No", typeof(string), "TelNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("WorkTel5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Mobile1Code", typeof(string)));
            Add(new OutputColumn("Mobile1No", typeof(string), "MobileNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("Mobile1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Mobile2Code", typeof(string)));
            Add(new OutputColumn("Mobile2No", typeof(string), "MobileNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("Mobile2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Mobile3Code", typeof(string)));
            Add(new OutputColumn("Mobile3No", typeof(string), "MobileNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("Mobile3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Mobile4Code", typeof(string)));
            Add(new OutputColumn("Mobile4No", typeof(string), "MobileNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("Mobile4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Mobile5Code", typeof(string)));
            Add(new OutputColumn("Mobile5No", typeof(string), "MobileNumberRule", _rulesService.IsValid));
            Add(new OutputColumn("Mobile5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Email1", typeof(string), "EmailAddressRule", _rulesService.IsValid));
            Add(new OutputColumn("Email1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Email2", typeof(string), "EmailAddressRule", _rulesService.IsValid));
            Add(new OutputColumn("Email2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Email3", typeof(string), "EmailAddressRule", _rulesService.IsValid));
            Add(new OutputColumn("Email3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Email4", typeof(string), "EmailAddressRule", _rulesService.IsValid));
            Add(new OutputColumn("Email4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Email5", typeof(string), "EmailAddressRule", _rulesService.IsValid));
            Add(new OutputColumn("Email5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Address1YearsOfTenure", typeof(int)));
            Add(new OutputColumn("Address1Line1", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1Line2", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1Line3", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1Line4", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1Suburb", typeof(string)));
            Add(new OutputColumn("Address1CityState", typeof(string)));
            Add(new OutputColumn("Address1Province", typeof(string)));
            Add(new OutputColumn("Address1PostalCode", typeof(string), "PostalCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1Country", typeof(string), "CountryCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address1OwnershipStatus", typeof(string)));
            Add(new OutputColumn("Address1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Address2YearsOfTenure", typeof(int)));
            Add(new OutputColumn("Address2Line1", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2Line2", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2Line3", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2Line4", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2Suburb", typeof(string)));
            Add(new OutputColumn("Address2CityState", typeof(string)));
            Add(new OutputColumn("Address2Province", typeof(string)));
            Add(new OutputColumn("Address2PostalCode", typeof(string), "PostalCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2Country", typeof(string), "CountryCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address2OwnershipStatus", typeof(string)));
            Add(new OutputColumn("Address2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Address3YearsOfTenure", typeof(int)));
            Add(new OutputColumn("Address3Line1", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3Line2", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3Line3", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3Line4", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3Suburb", typeof(string)));
            Add(new OutputColumn("Address3CityState", typeof(string)));
            Add(new OutputColumn("Address3Province", typeof(string)));
            Add(new OutputColumn("Address3PostalCode", typeof(string), "PostalCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3Country", typeof(string), "CountryCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address3OwnershipStatus", typeof(string)));
            Add(new OutputColumn("Address3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Address4YearsOfTenure", typeof(int)));
            Add(new OutputColumn("Address4Line1", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4Line2", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4Line3", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4Line4", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4Suburb", typeof(string)));
            Add(new OutputColumn("Address4CityState", typeof(string)));
            Add(new OutputColumn("Address4Province", typeof(string)));
            Add(new OutputColumn("Address4PostalCode", typeof(string), "PostalCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4Country", typeof(string), "CountryCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address4OwnershipStatus", typeof(string)));
            Add(new OutputColumn("Address4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Address5YearsOfTenure", typeof(int)));
            Add(new OutputColumn("Address5Line1", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5Line2", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5Line3", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5Line4", typeof(string), "AddressLineRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5Suburb", typeof(string)));
            Add(new OutputColumn("Address5CityState", typeof(string)));
            Add(new OutputColumn("Address5Province", typeof(string)));
            Add(new OutputColumn("Address5PostalCode", typeof(string), "PostalCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5Country", typeof(string), "CountryCodeRule", _rulesService.IsValid));
            Add(new OutputColumn("Address5OwnershipStatus", typeof(string)));
            Add(new OutputColumn("Address5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("IsDeceased", typeof(bool)));
            Add(new OutputColumn("IsDeceasedDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("IsOptOut", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("IsOptOutDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Age", typeof(int)));
            Add(new OutputColumn("MaritalStatus", typeof(string), "MaritalStatusRule", _rulesService.IsValid));
            Add(new OutputColumn("MaritalStatusDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Income", typeof(int)));
            Add(new OutputColumn("IncomeDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Gender", typeof(string), "GenderRule", _rulesService.IsValid));
            Add(new OutputColumn("GenderDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("ClusterGroup", typeof(string)));
            Add(new OutputColumn("ClusterGroupDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("LSM", typeof(string)));
            Add(new OutputColumn("LSMDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeOwner", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("HomeOwnerDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("VehicleOwner", typeof(bool)));
            Add(new OutputColumn("VehicleOwnerDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("PerformanceIndicator", typeof(string)));
            Add(new OutputColumn("PerformanceIndicatorDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("SpouseName", typeof(string)));
            Add(new OutputColumn("SpouseNameDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer1", typeof(string)));
            Add(new OutputColumn("Employer1DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer1Occupation", typeof(string)));
            Add(new OutputColumn("Employer1OccupationDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer2", typeof(string)));
            Add(new OutputColumn("Employer2DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer2Occupation", typeof(string)));
            Add(new OutputColumn("Employer2OccupationDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer3", typeof(string)));
            Add(new OutputColumn("Employer3DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer3Occupation", typeof(string)));
            Add(new OutputColumn("Employer3OccupationDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer4", typeof(string)));
            Add(new OutputColumn("Employer4DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer4Occupation", typeof(string)));
            Add(new OutputColumn("Employer4OccupationDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer5", typeof(string)));
            Add(new OutputColumn("Employer5DataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("Employer5Occupation", typeof(string)));
            Add(new OutputColumn("Employer5OccupationDataProviderLastUpdated", typeof(DateTime)) { InputFormat = "yyyyMMdd" });
            Add(new OutputColumn("ReservedForCampaign", typeof(string)));
        }
    }
}