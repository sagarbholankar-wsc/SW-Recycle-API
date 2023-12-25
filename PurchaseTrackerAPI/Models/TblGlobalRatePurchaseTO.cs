using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PurchaseTrackerAPI.StaticStuff;
using static PurchaseTrackerAPI.StaticStuff.Constants;

namespace PurchaseTrackerAPI.Models
{
    public class TblGlobalRatePurchaseTO
    {

        #region Declarations
        Int32 idGlobalRatePurchase;
        Int32 createdBy;
        DateTime createdOn;
        Double rate;
        String comments;
        Int32 rateReasonId;
        Int32 ratebandcosting;
        String rateReasonDesc;
        Double totalBookingQty;
        Double avgBookingRate;
        List<TblRateBandDeclarationPurchaseTO> rateBandDeclarationPurchaseTOList;
        #endregion

        #region Constructor
        public TblGlobalRatePurchaseTO()
        {
        }

        #endregion

        #region GetSet
        public Int32 IdGlobalRatePurchase
        {
            get { return idGlobalRatePurchase; }
            set { idGlobalRatePurchase = value; }
        }

        public Int32 Ratebandcosting
        {
            get { return ratebandcosting; }
            set { ratebandcosting = value; }
        }

        public Int32 CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        public String CreatedOnStr
        {
            get { return createdOn.ToString(Constants.DefaultDateFormat); }
        }
        public Double Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public String Comments
        {
            get { return comments; }
            set { comments = value; }
        }

        public int RateReasonId
        {
            get { return rateReasonId; }
            set { rateReasonId = value; }
        }

        public String RateReasonDesc
        {
            get { return rateReasonDesc; }
            set { rateReasonDesc = value; }
        }
        //public String CreatedOnStr
        //{
        //    get { return createdOn.ToString(Constants.DefaultDateFormat); }
        //}
        
        public List<TblRateBandDeclarationPurchaseTO> RateBandDeclarationPurchaseTOList { get => rateBandDeclarationPurchaseTOList; set => rateBandDeclarationPurchaseTOList = value; }
        public double TotalBookingQty { get => totalBookingQty; set => totalBookingQty = value; }
        public double AvgBookingRate { get => avgBookingRate; set => avgBookingRate = value; }
        #endregion
    }

    public class TblPurchaseQuotaTO
    {

        #region Declarations
        Int32 idQuota;       
        Double quotaQty;
        Double pendingQty;
        Int32 createdBy;
        DateTime createdOn;
        Int32 isActive;
        Int32 quotaReasonId;

        List<TblPurchaseQuotaDetailsTO> purchaseQuotaDetailsToList;
        #endregion

        #region Constructor
        public TblPurchaseQuotaTO()
        {
        }

        #endregion

        #region GetSet
        public Int32 IdQuota
        {
            get { return idQuota; }
            set { idQuota = value; }
        }
        public Double QuotaQty
        {
            get { return quotaQty; }
            set { quotaQty = value; }
        }
        public Double PendingQty
        {
            get { return pendingQty; }
            set { pendingQty = value; }
        }
        public Int32 CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        public Int32 IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public Int32 QuotaReasonId
        {
            get { return quotaReasonId; }
            set { quotaReasonId = value; }
        }

        


        public List<TblPurchaseQuotaDetailsTO> PurchaseQuotaDetailsToList { get => purchaseQuotaDetailsToList; set => purchaseQuotaDetailsToList = value; }
       
        #endregion
    }
    public class TblPurchaseQuotaDetailsTO
    {

        #region Declarations
        Int32 idQuotaDetails;
        Int32 quotaId;
        Int32 purchaseManagerId;
        string  purchaseManager;
        Double quotaQty;
        Double pendingQty;
        string transferpurchaseManagerIdStr;
        string transferQtyStr;
        Int32 isActive;
        Int32 transferedBy;
        DateTime createdOn;
        Int32 purchaseManagerSourceId;
        Int32 purchaseManagerDesnId;
        Double transferQty;


        #endregion

        #region Constructor
        public TblPurchaseQuotaDetailsTO()
        {
        }

        #endregion

        #region GetSet
        public Int32 IdQuotaDetails
        {
            get { return idQuotaDetails; }
            set { idQuotaDetails = value; }
        }
        public Int32 QuotaId
        {
            get { return quotaId; }
            set { quotaId = value; }
        }
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }
        public Int32 PurchaseManagerId
        {
            get { return purchaseManagerId; }
            set { purchaseManagerId = value; }
        }
        public Double QuotaQty
        {
            get { return quotaQty; }
            set { quotaQty = value; }
        }
        public Double PendingQty
        {
            get { return pendingQty; }
            set { pendingQty = value; }
        }
        public string TransferpurchaseManagerIdStr
        {
            get { return transferpurchaseManagerIdStr; }
            set { transferpurchaseManagerIdStr = value; }
        }

        public string TransferQtyStr
        {
            get { return transferQtyStr; }
            set { transferQtyStr = value; }
        }
       
        public Int32 IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public Int32 TransferedBy
        {
            get { return transferedBy; }
            set { transferedBy = value; }
        }
        public Int32 PurchaseManagerSourceId
        {
            get { return purchaseManagerSourceId; }
            set { purchaseManagerSourceId = value; }
        }
        public Int32 PurchaseManagerDesnId
        {
            get { return purchaseManagerDesnId; }
            set { purchaseManagerDesnId = value; }
        }
        public Double TransferQty
        {
            get { return transferQty; }
            set { transferQty = value; }
        }

        public string PurchaseManager
        {
            get { return purchaseManager; }
            set { purchaseManager = value; }
        }

        



        #endregion
    }
}
