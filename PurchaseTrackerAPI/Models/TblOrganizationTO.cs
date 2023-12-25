using Newtonsoft.Json;
using PurchaseTrackerAPI.StaticStuff;
using System;
using System.Collections.Generic;
using System.Text;
using static PurchaseTrackerAPI.StaticStuff.Constants;
using ODLMSWebAPI.Models;

namespace PurchaseTrackerAPI.Models
{
    public class TblOrganizationTO
    {
        #region Declarations
        Int32 idOrganization;
        Int32 orgTypeId;
        Int32 addrId;
        Int32 firstOwnerPersonId;
        Int32 secondOwnerPersonId;
        Int32 parentId;
     
        Int32 createdBy;
       
        DateTime createdOn;
        String firmName;

        Double lastAllocQty;
        Double lastRateBand;

        TblCompetitorUpdatesTO tblCompetitorUpdatesTO;

        Double declaredRate;
        Int32 validUpto;
        Double balanceQuota;
        Int32 quotaDeclarationId;

        List<TblAddressTO> addressList;
        List<TblPersonTO> personList;
        List<TblOrgLicenseDtlTO> orgLicenseDtlTOList;
        List<TblCnfDealersTO> cnfDealersTOList;
        List<TblCompetitorExtTO> competitorExtTOList;

        Int32 updatedBy;
        DateTime updatedOn;
        String firstOwnerName;
        String secondOwnerName;

        String phoneNo;
        String faxNo;
        String emailAddr;
        String website;
        String registeredMobileNos;

        Int32 cdStructureId;
        Double cdStructure;
        Int32 isActive;
        String remark;
        Int32 delPeriodId;
        Int32 deliveryPeriod;
        String villageName;
        Int32 isSpecialCnf;
        String digitalSign;
        DateTime deactivatedOn;

        Int32 districtId;
        Int32 orgStatusId;
        /// <summary>
        /// Vijaymla Added to set organization logo,other details bank details for  invoice print
        ///</summary>
        String orgLogo;
        List<TblInvoiceOtherDetailsTO> invoiceOtherDetailsTOList;
        List<TblInvoiceBankDetailsTO> invoiceBankDetailsTOList;

        List<BrandRateDtlTO> brandRateDtlTOList;

        String overdueRefId;
        String enqRefId;
        Int32 isBlocked;
        string orgStatusName;

        //Int32 isBlocked;
        //string orgStatusName;
        string orgStatusColor;

        #endregion

        #region Constructor
        public TblOrganizationTO()
        {
        }

        #endregion

        #region GetSet
        public Int32 IdOrganization
        {
            get { return idOrganization; }
            set { idOrganization = value; }
        }
        public Int32 OrgTypeId
        {
            get { return orgTypeId; }
            set { orgTypeId = value; }
        }
        public Int32 AddrId
        {
            get { return addrId; }
            set { addrId = value; }
        }
        public Int32 FirstOwnerPersonId
        {
            get { return firstOwnerPersonId; }
            set { firstOwnerPersonId = value; }
        }
        public Int32 SecondOwnerPersonId
        {
            get { return secondOwnerPersonId; }
            set { secondOwnerPersonId = value; }
        }
        public Int32 ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
        [JsonIgnore]
        public Int32 CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        [JsonIgnore]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        public String FirmName
        {
            get { return firmName; }
            set { firmName = value; }
        }

        /// <summary>
        /// Sanjay [2017-02-09] In case of C&F Agents , Show last allocated qty and Rate Band
        /// </summary>

        public Double LastAllocQty
        {
            get { return lastAllocQty; }
            set { lastAllocQty = value; }
        }

        /// <summary>
        /// Sanjay [2017-02-09] In case of C&F Agents , Show last allocated qty and Rate Band
        /// </summary>
        public Double LastRateBand
        {
            get { return lastRateBand; }
            set { lastRateBand = value; }
        }


        public TblCompetitorUpdatesTO CompetitorUpdatesTO
        {
            get
            {
                return tblCompetitorUpdatesTO;
            }

            set
            {
                tblCompetitorUpdatesTO = value;
            }
        }

        public Constants.OrgTypeE OrgTypeE
        {
            get
            {
                OrgTypeE orgTypeE = OrgTypeE.TRANSPOTER;
                if (Enum.IsDefined(typeof(OrgTypeE), orgTypeId))
                {
                    orgTypeE = (OrgTypeE)orgTypeId;
                }
                return orgTypeE;

            }
            set
            {
                orgTypeId = (int)value;
            }
        }

        /// <summary>
        /// Sanjay [2017-02-10] This is not DB Field or Organization.
        /// When data is posted then it should in single Object Model.
        /// This will be saved into global rate declaration.
        /// </summary>
        public Double DeclaredRate
        {
            get { return declaredRate; }
            set { declaredRate = value; }
        }


        /// <summary>
        /// Sanjay [2017-02-22] These fields are used when showing balance quota for C&F
        /// while declaring the quota qty. Used only for data retrival
        /// </summary>
        public Double BalanceQuota
        {
            get { return balanceQuota; }
            set { balanceQuota = value; }
        }

        /// <summary>
        /// Sanjay [2017-02-22] These fields are used when showing balance quota for C&F
        /// while declaring the quota qty. Used only for data retrival
        /// </summary>
        public Int32 ValidUpto
        {
            get { return validUpto; }
            set { validUpto = value; }
        }
        public Int32 QuotaDeclarationId
        {
            get { return quotaDeclarationId; }
            set { quotaDeclarationId = value; }
        }

        public Int32 UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        public DateTime UpdatedOn
        {
            get { return updatedOn; }
            set { updatedOn = value; }
        }
        public List<TblAddressTO> AddressList
        {
            get
            {
                return addressList;
            }

            set
            {
                addressList = value;
            }
        }

        public List<TblPersonTO> PersonList
        {
            get
            {
                return personList;
            }

            set
            {
                personList = value;
            }
        }

        public string FirstOwnerName
        {
            get
            {
                return firstOwnerName;
            }

            set
            {
                firstOwnerName = value;
            }
        }

        public string SecondOwnerName
        {
            get
            {
                return secondOwnerName;
            }

            set
            {
                secondOwnerName = value;
            }
        }

        public string PhoneNo
        {
            get
            {
                return phoneNo;
            }

            set
            {
                phoneNo = value;
            }
        }

        public string FaxNo
        {
            get
            {
                return faxNo;
            }

            set
            {
                faxNo = value;
            }
        }

        public string EmailAddr
        {
            get
            {
                return emailAddr;
            }

            set
            {
                emailAddr = value;
            }
        }

        public string Website
        {
            get
            {
                return website;
            }

            set
            {
                website = value;
            }
        }

        public string RegisteredMobileNos { get => registeredMobileNos; set => registeredMobileNos = value; }
        public int CdStructureId { get => cdStructureId; set => cdStructureId = value; }
        public double CdStructure { get => cdStructure; set => cdStructure = value; }
        public int IsActive { get => isActive; set => isActive = value; }
        public string Remark { get => remark; set => remark = value; }
        public int DelPeriodId { get => delPeriodId; set => delPeriodId = value; }
        public int DeliveryPeriod { get => deliveryPeriod; set => deliveryPeriod = value; }
        public List<TblOrgLicenseDtlTO> OrgLicenseDtlTOList { get => orgLicenseDtlTOList; set => orgLicenseDtlTOList = value; }
        public string VillageName { get => villageName; set => villageName = value; }
        public int IsSpecialCnf { get => isSpecialCnf; set => isSpecialCnf = value; }
        public List<TblCnfDealersTO> CnfDealersTOList { get => cnfDealersTOList; set => cnfDealersTOList = value; }
        public List<TblCompetitorExtTO> CompetitorExtTOList { get => competitorExtTOList; set => competitorExtTOList = value; }
        public string DigitalSign { get => digitalSign; set => digitalSign = value; }
        public DateTime DeactivatedOn { get => deactivatedOn; set => deactivatedOn = value; }
        public int DistrictId { get => districtId; set => districtId = value; }

        /// <summary>
        /// Vijaymala [2017-30-10] added this field to save organization logo,invoice other details like
        /// declaration or terms and condition used in footer of invoice print
        /// </summary>
        /// 
        public String OrgLogo
        {
            get { return orgLogo; }
            set { orgLogo = value; }
        }
        public List<TblInvoiceOtherDetailsTO>InvoiceOtherDetailsTOList { get => invoiceOtherDetailsTOList; set => invoiceOtherDetailsTOList = value; }
        public List<TblInvoiceBankDetailsTO> InvoiceBankDetailsTOList { get => invoiceBankDetailsTOList; set => invoiceBankDetailsTOList = value; }
        public List<BrandRateDtlTO> BrandRateDtlTOList { get => brandRateDtlTOList; set => brandRateDtlTOList = value; }


        public String OverdueRefId
        {
            get { return overdueRefId; }
            set { overdueRefId = value; }
        }

        public String EnqRefId
        {
            get { return enqRefId; }
            set { enqRefId = value; }
        }
        public Int32 OrgStatusId
        {
            get { return orgStatusId; }
            set { orgStatusId = value; }
        }

        public Int32 IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }

        public string OrgStatusName
        {
            get { return orgStatusName; }
            set { orgStatusName = value; }
        }

        public string OrgStatusColor
        {
            get { return orgStatusColor; }
            set { orgStatusColor = value; }
        }

        
        #endregion
    }
}
