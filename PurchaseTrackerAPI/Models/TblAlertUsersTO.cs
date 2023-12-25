using PurchaseTrackerAPI.StaticStuff;
using System;
using System.Collections.Generic;
using System.Text;
using static PurchaseTrackerAPI.StaticStuff.Constants;

namespace PurchaseTrackerAPI.Models
{
    public class TblAlertUsersTO
    {
        #region Declarations
        Int32 idAlertUser;
        Int32 alertInstanceId;
        Int32 userId;
        Int32 roleId;
        List<TblAlertSubscriptSettingsTO> alertSubscriptSettingsTOList;
        String raisedByUserName;
        DateTime raisedOn;
        String alertComment;
        Int32 isAcknowledged;
        Int32 isReseted;
        Int32 alertDefinitionId;
        String deviceId;
        String navigationUrl;
        #endregion

        #region Constructor
        public TblAlertUsersTO()
        {
        }

        #endregion

        #region GetSet
        public Int32 IdAlertUser
        {
            get { return idAlertUser; }
            set { idAlertUser = value; }
        }
        public Int32 AlertInstanceId
        {
            get { return alertInstanceId; }
            set { alertInstanceId = value; }
        }
        public Int32 UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public Int32 RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        public List<TblAlertSubscriptSettingsTO> AlertSubscriptSettingsTOList
        {
            get
            {
                return alertSubscriptSettingsTOList;
            }

            set
            {
                alertSubscriptSettingsTOList = value;
            }
        }

        public string RaisedByUserName { get => raisedByUserName; set => raisedByUserName = value; }
        public string AlertComment { get => alertComment; set => alertComment = value; }
        public DateTime RaisedOn { get => raisedOn; set => raisedOn = value; }
        public String RaisedOnStr
        {
            get { return raisedOn.ToString(Constants.DefaultDateFormat); }
        }

        public int IsAcknowledged { get => isAcknowledged; set => isAcknowledged = value; }
        public int IsReseted { get => isReseted; set => isReseted = value; }
        public int AlertDefinitionId { get => alertDefinitionId; set => alertDefinitionId = value; }
        public string DeviceId { get => deviceId; set => deviceId = value; }
        public string NavigationUrl { get => navigationUrl; set => navigationUrl = value; }
        #endregion
    }
}
