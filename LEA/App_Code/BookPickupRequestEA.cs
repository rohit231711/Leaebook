﻿using System.Xml.Serialization;

[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public class BookPickupRequestEA
{

    private Request requestField;

    private Requestor requestorField;

    private Place placeField;

    private Pickup pickupField;

    private Contact pickupContactField;

    private ShipmentDetails[] shipmentDetailsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Requestor Requestor
    {
        get { return this.requestorField; }
        set { this.requestorField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Place Place
    {
        get { return this.placeField; }
        set { this.placeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Pickup Pickup
    {
        get { return this.pickupField; }
        set { this.pickupField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Contact PickupContact
    {
        get { return this.pickupContactField; }
        set { this.pickupContactField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ShipmentDetails",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentDetails[] ShipmentDetails
    {
        get { return this.shipmentDetailsField; }
        set { this.shipmentDetailsField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Request
{

    private ServiceHeader serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class ServiceHeader
{

    private System.DateTime messageTimeField;

    private bool messageTimeFieldSpecified;

    private string messageReferenceField;

    private string siteIDField;

    private string passwordField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.DateTime MessageTime
    {
        get { return this.messageTimeField; }
        set { this.messageTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MessageTimeSpecified
    {
        get { return this.messageTimeFieldSpecified; }
        set { this.messageTimeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MessageReference
    {
        get { return this.messageReferenceField; }
        set { this.messageReferenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SiteID
    {
        get { return this.siteIDField; }
        set { this.siteIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Password
    {
        get { return this.passwordField; }
        set { this.passwordField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class Piece
{

    private decimal weightField;

    private bool weightFieldSpecified;

    private string widthField;

    private string heightField;

    private string depthField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightSpecified
    {
        get { return this.weightFieldSpecified; }
        set { this.weightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Width
    {
        get { return this.widthField; }
        set { this.widthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Height
    {
        get { return this.heightField; }
        set { this.heightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Depth
    {
        get { return this.depthField; }
        set { this.depthField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class ChargeCard
{

    private string chargeCardNoField;

    private ChargeCardType chargeCardTypeField;

    private bool chargeCardTypeFieldSpecified;

    private string chargeCardExpiryDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ChargeCardNo
    {
        get { return this.chargeCardNoField; }
        set { this.chargeCardNoField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChargeCardType ChargeCardType
    {
        get { return this.chargeCardTypeField; }
        set { this.chargeCardTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeCardTypeSpecified
    {
        get { return this.chargeCardTypeFieldSpecified; }
        set { this.chargeCardTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ChargeCardExpiryDate
    {
        get { return this.chargeCardExpiryDateField; }
        set { this.chargeCardExpiryDateField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum ChargeCardType
{

    /// <remarks/>
    AM,

    /// <remarks/>
    DC,

    /// <remarks/>
    DI,

    /// <remarks/>
    MC,

    /// <remarks/>
    VI,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class ShipmentDetails
{

    private ShipmentDetailsAccountType accountTypeField;

    private bool accountTypeFieldSpecified;

    private string accountNumberField;

    private ChargeCard chargeCardField;

    private string billToAccountNumberField;

    private string aWBNumberField;

    private string numberOfPiecesField;

    private decimal weightField;

    private WeightUnit weightUnitField;

    private string globalProductCodeField;

    private DoorTo doorToField;

    private DimensionUnit dimensionUnitField;

    private bool dimensionUnitFieldSpecified;

    private string insuredAmountField;

    private string insuredCurrencyCodeField;

    private Piece piecesField;

    private string[] specialServiceField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentDetailsAccountType AccountType
    {
        get { return this.accountTypeField; }
        set { this.accountTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool AccountTypeSpecified
    {
        get { return this.accountTypeFieldSpecified; }
        set { this.accountTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string AccountNumber
    {
        get { return this.accountNumberField; }
        set { this.accountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChargeCard ChargeCard
    {
        get { return this.chargeCardField; }
        set { this.chargeCardField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string BillToAccountNumber
    {
        get { return this.billToAccountNumberField; }
        set { this.billToAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string AWBNumber
    {
        get { return this.aWBNumberField; }
        set { this.aWBNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string NumberOfPieces
    {
        get { return this.numberOfPiecesField; }
        set { this.numberOfPiecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightUnit WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DoorTo DoorTo
    {
        get { return this.doorToField; }
        set { this.doorToField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DimensionUnit DimensionUnit
    {
        get { return this.dimensionUnitField; }
        set { this.dimensionUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DimensionUnitSpecified
    {
        get { return this.dimensionUnitFieldSpecified; }
        set { this.dimensionUnitFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InsuredAmount
    {
        get { return this.insuredAmountField; }
        set { this.insuredAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InsuredCurrencyCode
    {
        get { return this.insuredCurrencyCodeField; }
        set { this.insuredCurrencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Piece Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SpecialService",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] SpecialService
    {
        get { return this.specialServiceField; }
        set { this.specialServiceField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/pickupdatatypes_EA"
    )]
public enum ShipmentDetailsAccountType
{

    /// <remarks/>
    D,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum WeightUnit
{

    /// <remarks/>
    K,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum DoorTo
{

    /// <remarks/>
    DD,

    /// <remarks/>
    DA,

    /// <remarks/>
    AA,

    /// <remarks/>
    DC,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum DimensionUnit
{

    /// <remarks/>
    C,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class Contact
{

    private string personNameField;

    private string phoneField;

    private string phoneExtensionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PersonName
    {
        get { return this.personNameField; }
        set { this.personNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Phone
    {
        get { return this.phoneField; }
        set { this.phoneField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneExtension
    {
        get { return this.phoneExtensionField; }
        set { this.phoneExtensionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class WeightSeg
{

    private decimal weightField;

    private WeightUnit weightUnitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightUnit WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class Pickup
{

    private System.DateTime pickupDateField;

    private string readyByTimeField;

    private string closeTimeField;

    private string piecesField;

    private WeightSeg weightField;

    private string specialInstructionsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime PickupDate
    {
        get { return this.pickupDateField; }
        set { this.pickupDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReadyByTime
    {
        get { return this.readyByTimeField; }
        set { this.readyByTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CloseTime
    {
        get { return this.closeTimeField; }
        set { this.closeTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightSeg weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialInstructions
    {
        get { return this.specialInstructionsField; }
        set { this.specialInstructionsField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class Place
{

    private PlaceLocationType locationTypeField;

    private string companyNameField;

    private string address1Field;

    private string address2Field;

    private string address3Field;

    private string packageLocationField;

    private string cityField;

    private string countryCodeField;

    private string stateCodeField;

    private string divisionNameField;

    private string postalCodeField;

    private string routeCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PlaceLocationType LocationType
    {
        get { return this.locationTypeField; }
        set { this.locationTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address1
    {
        get { return this.address1Field; }
        set { this.address1Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address2
    {
        get { return this.address2Field; }
        set { this.address2Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address3
    {
        get { return this.address3Field; }
        set { this.address3Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PackageLocation
    {
        get { return this.packageLocationField; }
        set { this.packageLocationField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StateCode
    {
        get { return this.stateCodeField; }
        set { this.stateCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DivisionName
    {
        get { return this.divisionNameField; }
        set { this.divisionNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RouteCode
    {
        get { return this.routeCodeField; }
        set { this.routeCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/pickupdatatypes_EA"
    )]
public enum PlaceLocationType
{

    /// <remarks/>
    B,

    /// <remarks/>
    R,

    /// <remarks/>
    C,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class RequestorContact
{

    private string personNameField;

    private string phoneField;

    private string phoneExtensionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PersonName
    {
        get { return this.personNameField; }
        set { this.personNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Phone
    {
        get { return this.phoneField; }
        set { this.phoneField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneExtension
    {
        get { return this.phoneExtensionField; }
        set { this.phoneExtensionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/pickupdatatypes_EA")]
public partial class Requestor
{

    private RequestorAccountType accountTypeField;

    private string accountNumberField;

    private RequestorContact requestorContactField;

    private string companyNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public RequestorAccountType AccountType
    {
        get { return this.accountTypeField; }
        set { this.accountTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string AccountNumber
    {
        get { return this.accountNumberField; }
        set { this.accountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public RequestorContact RequestorContact
    {
        get { return this.requestorContactField; }
        set { this.requestorContactField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/pickupdatatypes_EA"
    )]
public enum RequestorAccountType
{

    /// <remarks/>
    D,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class CancelPickupRequestEA
{

    private Request requestField;

    private string confirmationNumberField;

    private string requestorNameField;

    private CancelPickupRequestEAReason reasonField;

    private bool reasonFieldSpecified;

    private System.DateTime pickupDateField;

    private string countryCodeField;

    private string cancelTimeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ConfirmationNumber
    {
        get { return this.confirmationNumberField; }
        set { this.confirmationNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RequestorName
    {
        get { return this.requestorNameField; }
        set { this.requestorNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CancelPickupRequestEAReason Reason
    {
        get { return this.reasonField; }
        set { this.reasonField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ReasonSpecified
    {
        get { return this.reasonFieldSpecified; }
        set { this.reasonFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime PickupDate
    {
        get { return this.pickupDateField; }
        set { this.pickupDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CancelTime
    {
        get { return this.cancelTimeField; }
        set { this.cancelTimeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
public enum CancelPickupRequestEAReason
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("001")]
    Item001,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("002")]
    Item002,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("003")]
    Item003,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("004")]
    Item004,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("005")]
    Item005,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("006")]
    Item006,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("007")]
    Item007,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class DCTRequest
{

    private object itemField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GetCapability", typeof(DCTRequestGetCapability),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("GetQuote", typeof(DCTRequestGetQuote),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object Item
    {
        get { return this.itemField; }
        set { this.itemField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTRequestGetCapability
{

    private Request1 requestField;

    private DCTFrom fromField;

    private BkgDetailsType bkgDetailsField;

    private DCTTo toField;

    private DCTDutiable dutiableField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request1 Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTFrom From
    {
        get { return this.fromField; }
        set { this.fromField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsType BkgDetails
    {
        get { return this.bkgDetailsField; }
        set { this.bkgDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTTo To
    {
        get { return this.toField; }
        set { this.toField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTDutiable Dutiable
    {
        get { return this.dutiableField; }
        set { this.dutiableField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Request", Namespace = "http://www.dhl.com/datatypes")]
public partial class Request1
{

    private ServiceHeader1 serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader1 ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ServiceHeader", Namespace = "http://www.dhl.com/datatypes")]
public partial class ServiceHeader1
{

    private System.DateTime messageTimeField;

    private bool messageTimeFieldSpecified;

    private string messageReferenceField;

    private string siteIDField;

    private string passwordField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.DateTime MessageTime
    {
        get { return this.messageTimeField; }
        set { this.messageTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MessageTimeSpecified
    {
        get { return this.messageTimeFieldSpecified; }
        set { this.messageTimeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MessageReference
    {
        get { return this.messageReferenceField; }
        set { this.messageReferenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SiteID
    {
        get { return this.siteIDField; }
        set { this.siteIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Password
    {
        get { return this.passwordField; }
        set { this.passwordField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class DCTFrom
{

    private string countryCodeField;

    private string postalcodeField;

    private string cityField;

    private string suburbField;

    private string vatNoField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Postalcode
    {
        get { return this.postalcodeField; }
        set { this.postalcodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Suburb
    {
        get { return this.suburbField; }
        set { this.suburbField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string VatNo
    {
        get { return this.vatNoField; }
        set { this.vatNoField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class BkgDetailsType
{

    private string paymentCountryCodeField;

    private System.DateTime dateField;

    private string readyTimeField;

    private string readyTimeGMTOffsetField;

    private BkgDetailsTypeDimensionUnit dimensionUnitField;

    private BkgDetailsTypeWeightUnit weightUnitField;

    private string[] numberOfPiecesField;

    private decimal shipmentWeightField;

    private bool shipmentWeightFieldSpecified;

    private decimal volumeField;

    private bool volumeFieldSpecified;

    private decimal maxPieceWeightField;

    private bool maxPieceWeightFieldSpecified;

    private decimal maxPieceHeightField;

    private bool maxPieceHeightFieldSpecified;

    private decimal maxPieceDepthField;

    private bool maxPieceDepthFieldSpecified;

    private decimal maxPieceWidthField;

    private bool maxPieceWidthFieldSpecified;

    private PieceType[] piecesField;

    private string paymentAccountNumberField;

    private BkgDetailsTypeIsDutiable isDutiableField;

    private bool isDutiableFieldSpecified;

    private BkgDetailsTypeNetworkTypeCode networkTypeCodeField;

    private bool networkTypeCodeFieldSpecified;

    private QtdShpType[] qtdShpField;

    private decimal cODAmountField;

    private bool cODAmountFieldSpecified;

    private string cODCurrencyCodeField;

    private string cODAccountNumberField;

    private decimal insuredValueField;

    private bool insuredValueFieldSpecified;

    private string insuredCurrencyField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PaymentCountryCode
    {
        get { return this.paymentCountryCodeField; }
        set { this.paymentCountryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime Date
    {
        get { return this.dateField; }
        set { this.dateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "duration")]
    public string ReadyTime
    {
        get { return this.readyTimeField; }
        set { this.readyTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReadyTimeGMTOffset
    {
        get { return this.readyTimeGMTOffsetField; }
        set { this.readyTimeGMTOffsetField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsTypeDimensionUnit DimensionUnit
    {
        get { return this.dimensionUnitField; }
        set { this.dimensionUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsTypeWeightUnit WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("NumberOfPieces",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
    public string[] NumberOfPieces
    {
        get { return this.numberOfPiecesField; }
        set { this.numberOfPiecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ShipmentWeight
    {
        get { return this.shipmentWeightField; }
        set { this.shipmentWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShipmentWeightSpecified
    {
        get { return this.shipmentWeightFieldSpecified; }
        set { this.shipmentWeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Volume
    {
        get { return this.volumeField; }
        set { this.volumeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool VolumeSpecified
    {
        get { return this.volumeFieldSpecified; }
        set { this.volumeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal MaxPieceWeight
    {
        get { return this.maxPieceWeightField; }
        set { this.maxPieceWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MaxPieceWeightSpecified
    {
        get { return this.maxPieceWeightFieldSpecified; }
        set { this.maxPieceWeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal MaxPieceHeight
    {
        get { return this.maxPieceHeightField; }
        set { this.maxPieceHeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MaxPieceHeightSpecified
    {
        get { return this.maxPieceHeightFieldSpecified; }
        set { this.maxPieceHeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal MaxPieceDepth
    {
        get { return this.maxPieceDepthField; }
        set { this.maxPieceDepthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MaxPieceDepthSpecified
    {
        get { return this.maxPieceDepthFieldSpecified; }
        set { this.maxPieceDepthFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal MaxPieceWidth
    {
        get { return this.maxPieceWidthField; }
        set { this.maxPieceWidthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MaxPieceWidthSpecified
    {
        get { return this.maxPieceWidthFieldSpecified; }
        set { this.maxPieceWidthFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Piece", Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public PieceType[] Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PaymentAccountNumber
    {
        get { return this.paymentAccountNumberField; }
        set { this.paymentAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsTypeIsDutiable IsDutiable
    {
        get { return this.isDutiableField; }
        set { this.isDutiableField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsDutiableSpecified
    {
        get { return this.isDutiableFieldSpecified; }
        set { this.isDutiableFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsTypeNetworkTypeCode NetworkTypeCode
    {
        get { return this.networkTypeCodeField; }
        set { this.networkTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool NetworkTypeCodeSpecified
    {
        get { return this.networkTypeCodeFieldSpecified; }
        set { this.networkTypeCodeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdShp", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public QtdShpType[] QtdShp
    {
        get { return this.qtdShpField; }
        set { this.qtdShpField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal CODAmount
    {
        get { return this.cODAmountField; }
        set { this.cODAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CODAmountSpecified
    {
        get { return this.cODAmountFieldSpecified; }
        set { this.cODAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CODCurrencyCode
    {
        get { return this.cODCurrencyCodeField; }
        set { this.cODCurrencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CODAccountNumber
    {
        get { return this.cODAccountNumberField; }
        set { this.cODAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal InsuredValue
    {
        get { return this.insuredValueField; }
        set { this.insuredValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool InsuredValueSpecified
    {
        get { return this.insuredValueFieldSpecified; }
        set { this.insuredValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InsuredCurrency
    {
        get { return this.insuredCurrencyField; }
        set { this.insuredCurrencyField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public enum BkgDetailsTypeDimensionUnit
{

    /// <remarks/>
    IN,

    /// <remarks/>
    CM,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public enum BkgDetailsTypeWeightUnit
{

    /// <remarks/>
    KG,

    /// <remarks/>
    LB,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class PieceType
{

    private string pieceIDField;

    private PieceTypePackageTypeCode packageTypeCodeField;

    private decimal heightField;

    private bool heightFieldSpecified;

    private decimal depthField;

    private bool depthFieldSpecified;

    private decimal widthField;

    private bool widthFieldSpecified;

    private decimal weightField;

    public PieceType()
    {
        this.packageTypeCodeField = PieceTypePackageTypeCode.BOX;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceID
    {
        get { return this.pieceIDField; }
        set { this.pieceIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.ComponentModel.DefaultValueAttribute(PieceTypePackageTypeCode.BOX)]
    public PieceTypePackageTypeCode PackageTypeCode
    {
        get { return this.packageTypeCodeField; }
        set { this.packageTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Height
    {
        get { return this.heightField; }
        set { this.heightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool HeightSpecified
    {
        get { return this.heightFieldSpecified; }
        set { this.heightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Depth
    {
        get { return this.depthField; }
        set { this.depthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DepthSpecified
    {
        get { return this.depthFieldSpecified; }
        set { this.depthFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Width
    {
        get { return this.widthField; }
        set { this.widthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WidthSpecified
    {
        get { return this.widthFieldSpecified; }
        set { this.widthFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public enum PieceTypePackageTypeCode
{

    /// <remarks/>
    FLY,

    /// <remarks/>
    COY,

    /// <remarks/>
    NCY,

    /// <remarks/>
    PAL,

    /// <remarks/>
    DBL,

    /// <remarks/>
    BOX,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public enum BkgDetailsTypeIsDutiable
{

    /// <remarks/>
    Y,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public enum BkgDetailsTypeNetworkTypeCode
{

    /// <remarks/>
    DD,

    /// <remarks/>
    TD,

    /// <remarks/>
    AL,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class QtdShpType
{

    private string globalProductCodeField;

    private string localProductCodeField;

    private QtdShpExChrgType[] qtdShpExChrgField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalProductCode
    {
        get { return this.localProductCodeField; }
        set { this.localProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdShpExChrg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified
        )]
    public QtdShpExChrgType[] QtdShpExChrg
    {
        get { return this.qtdShpExChrgField; }
        set { this.qtdShpExChrgField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class QtdShpExChrgType
{

    private string specialServiceTypeField;

    private string localSpecialServiceTypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialServiceType
    {
        get { return this.specialServiceTypeField; }
        set { this.specialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalSpecialServiceType
    {
        get { return this.localSpecialServiceTypeField; }
        set { this.localSpecialServiceTypeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class DCTTo
{

    private string countryCodeField;

    private string postalcodeField;

    private string cityField;

    private string suburbField;

    private string vatNoField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Postalcode
    {
        get { return this.postalcodeField; }
        set { this.postalcodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Suburb
    {
        get { return this.suburbField; }
        set { this.suburbField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string VatNo
    {
        get { return this.vatNoField; }
        set { this.vatNoField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTRequestdatatypes")]
public partial class DCTDutiable
{

    private string declaredCurrencyField;

    private float declaredValueField;

    private bool declaredValueFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeclaredCurrency
    {
        get { return this.declaredCurrencyField; }
        set { this.declaredCurrencyField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float DeclaredValue
    {
        get { return this.declaredValueField; }
        set { this.declaredValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DeclaredValueSpecified
    {
        get { return this.declaredValueFieldSpecified; }
        set { this.declaredValueFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTRequestGetQuote
{

    private Request1 requestField;

    private DCTFrom fromField;

    private BkgDetailsType bkgDetailsField;

    private DCTTo toField;

    private DCTDutiable dutiableField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request1 Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTFrom From
    {
        get { return this.fromField; }
        set { this.fromField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsType BkgDetails
    {
        get { return this.bkgDetailsField; }
        set { this.bkgDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTTo To
    {
        get { return this.toField; }
        set { this.toField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTDutiable Dutiable
    {
        get { return this.dutiableField; }
        set { this.dutiableField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class DCTResponse
{

    private object itemField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GetCapabilityResponse", typeof(DCTResponseGetCapabilityResponse)
        , Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("GetQuoteResponse", typeof(DCTResponseGetQuoteResponse),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object Item
    {
        get { return this.itemField; }
        set { this.itemField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTResponseGetCapabilityResponse
{

    private DCTResponseGetCapabilityResponseResponse responseField;

    private BkgDetailsType2[] bkgDetailsField;

    private SrvType[] srvsField;

    private NoteTypeCondition[][] noteField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTResponseGetCapabilityResponseResponse Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("BkgDetails", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsType2[] BkgDetails
    {
        get { return this.bkgDetailsField; }
        set { this.bkgDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Srv", Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public SrvType[] Srvs
    {
        get { return this.srvsField; }
        set { this.srvsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Condition", typeof(NoteTypeCondition),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public NoteTypeCondition[][] Note
    {
        get { return this.noteField; }
        set { this.noteField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTResponseGetCapabilityResponseResponse
{

    private ServiceHeader2 serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader2 ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ServiceHeader", Namespace = "http://www.dhl.com")]
public partial class ServiceHeader2
{

    private System.DateTime messageTimeField;

    private bool messageTimeFieldSpecified;

    private string messageReferenceField;

    private string siteIDField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.DateTime MessageTime
    {
        get { return this.messageTimeField; }
        set { this.messageTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool MessageTimeSpecified
    {
        get { return this.messageTimeFieldSpecified; }
        set { this.messageTimeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MessageReference
    {
        get { return this.messageReferenceField; }
        set { this.messageReferenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SiteID
    {
        get { return this.siteIDField; }
        set { this.siteIDField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "BkgDetailsType",
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class BkgDetailsType2
{

    private OrgnSvcAreaType originServiceAreaField;

    private DestSvcAreaType destinationServiceAreaField;

    private QtdShpType1[] qtdShpField;

    private string calcNextDayIndField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OrgnSvcAreaType OriginServiceArea
    {
        get { return this.originServiceAreaField; }
        set { this.originServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DestSvcAreaType DestinationServiceArea
    {
        get { return this.destinationServiceAreaField; }
        set { this.destinationServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdShp", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public QtdShpType1[] QtdShp
    {
        get { return this.qtdShpField; }
        set { this.qtdShpField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CalcNextDayInd
    {
        get { return this.calcNextDayIndField; }
        set { this.calcNextDayIndField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class OrgnSvcAreaType
{

    private string facilityCodeField;

    private string serviceAreaCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FacilityCode
    {
        get { return this.facilityCodeField; }
        set { this.facilityCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ServiceAreaCode
    {
        get { return this.serviceAreaCodeField; }
        set { this.serviceAreaCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class DestSvcAreaType
{

    private string facilityCodeField;

    private string serviceAreaCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FacilityCode
    {
        get { return this.facilityCodeField; }
        set { this.facilityCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ServiceAreaCode
    {
        get { return this.serviceAreaCodeField; }
        set { this.serviceAreaCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "QtdShpType",
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class QtdShpType1
{

    private string globalProductCodeField;

    private string localProductCodeField;

    private string productShortNameField;

    private string localProductNameField;

    private string networkTypeCodeField;

    private string pOfferedCustAgreementField;

    private string transIndField;

    private System.DateTime pickupDateField;

    private bool pickupDateFieldSpecified;

    private string pickupCutoffTimeField;

    private string bookingTimeField;

    private string currencyCodeField;

    private decimal exchangeRateField;

    private bool exchangeRateFieldSpecified;

    private decimal weightChargeField;

    private bool weightChargeFieldSpecified;

    private decimal weightChargeTaxField;

    private bool weightChargeTaxFieldSpecified;

    private decimal weightChargeTaxRateField;

    private bool weightChargeTaxRateFieldSpecified;

    private int totalTransitDaysField;

    private bool totalTransitDaysFieldSpecified;

    private int pickupPostalLocAddDaysField;

    private bool pickupPostalLocAddDaysFieldSpecified;

    private int deliveryPostalLocAddDaysField;

    private bool deliveryPostalLocAddDaysFieldSpecified;

    private string pickupNonDHLCourierCodeField;

    private string deliveryNonDHLCourierCodeField;

    private string deliveryCheckpointReturnField;

    private System.DateTime deliveryDateField;

    private bool deliveryDateFieldSpecified;

    private string deliveryTimeField;

    private string deliveryTimeGMTOffsetField;

    private decimal dimensionalWeightField;

    private bool dimensionalWeightFieldSpecified;

    private string weightUnitField;

    private string pickupDayOfWeekNumField;

    private string destinationDayOfWeekNumField;

    private QtdShpExChrgType1[] qtdShpExChrgField;

    private System.DateTime pricingDateField;

    private bool pricingDateFieldSpecified;

    private decimal shippingChargeField;

    private bool shippingChargeFieldSpecified;

    private decimal totalTaxAmountField;

    private bool totalTaxAmountFieldSpecified;

    private QtdSInAdCurType[] qtdSInAdCurField;

    private WeightChargeTaxDetType[] weightChargeTaxDetField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalProductCode
    {
        get { return this.localProductCodeField; }
        set { this.localProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ProductShortName
    {
        get { return this.productShortNameField; }
        set { this.productShortNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalProductName
    {
        get { return this.localProductNameField; }
        set { this.localProductNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NetworkTypeCode
    {
        get { return this.networkTypeCodeField; }
        set { this.networkTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string POfferedCustAgreement
    {
        get { return this.pOfferedCustAgreementField; }
        set { this.pOfferedCustAgreementField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TransInd
    {
        get { return this.transIndField; }
        set { this.transIndField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime PickupDate
    {
        get { return this.pickupDateField; }
        set { this.pickupDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PickupDateSpecified
    {
        get { return this.pickupDateFieldSpecified; }
        set { this.pickupDateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "duration")]
    public string PickupCutoffTime
    {
        get { return this.pickupCutoffTimeField; }
        set { this.pickupCutoffTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "duration")]
    public string BookingTime
    {
        get { return this.bookingTimeField; }
        set { this.bookingTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ExchangeRate
    {
        get { return this.exchangeRateField; }
        set { this.exchangeRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ExchangeRateSpecified
    {
        get { return this.exchangeRateFieldSpecified; }
        set { this.exchangeRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal WeightCharge
    {
        get { return this.weightChargeField; }
        set { this.weightChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightChargeSpecified
    {
        get { return this.weightChargeFieldSpecified; }
        set { this.weightChargeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal WeightChargeTax
    {
        get { return this.weightChargeTaxField; }
        set { this.weightChargeTaxField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightChargeTaxSpecified
    {
        get { return this.weightChargeTaxFieldSpecified; }
        set { this.weightChargeTaxFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal weightChargeTaxRate
    {
        get { return this.weightChargeTaxRateField; }
        set { this.weightChargeTaxRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool weightChargeTaxRateSpecified
    {
        get { return this.weightChargeTaxRateFieldSpecified; }
        set { this.weightChargeTaxRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int TotalTransitDays
    {
        get { return this.totalTransitDaysField; }
        set { this.totalTransitDaysField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TotalTransitDaysSpecified
    {
        get { return this.totalTransitDaysFieldSpecified; }
        set { this.totalTransitDaysFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int PickupPostalLocAddDays
    {
        get { return this.pickupPostalLocAddDaysField; }
        set { this.pickupPostalLocAddDaysField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PickupPostalLocAddDaysSpecified
    {
        get { return this.pickupPostalLocAddDaysFieldSpecified; }
        set { this.pickupPostalLocAddDaysFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int DeliveryPostalLocAddDays
    {
        get { return this.deliveryPostalLocAddDaysField; }
        set { this.deliveryPostalLocAddDaysField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DeliveryPostalLocAddDaysSpecified
    {
        get { return this.deliveryPostalLocAddDaysFieldSpecified; }
        set { this.deliveryPostalLocAddDaysFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PickupNonDHLCourierCode
    {
        get { return this.pickupNonDHLCourierCodeField; }
        set { this.pickupNonDHLCourierCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeliveryNonDHLCourierCode
    {
        get { return this.deliveryNonDHLCourierCodeField; }
        set { this.deliveryNonDHLCourierCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "duration")]
    public string DeliveryCheckpointReturn
    {
        get { return this.deliveryCheckpointReturnField; }
        set { this.deliveryCheckpointReturnField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime DeliveryDate
    {
        get { return this.deliveryDateField; }
        set { this.deliveryDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DeliveryDateSpecified
    {
        get { return this.deliveryDateFieldSpecified; }
        set { this.deliveryDateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "duration")]
    public string DeliveryTime
    {
        get { return this.deliveryTimeField; }
        set { this.deliveryTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeliveryTimeGMTOffset
    {
        get { return this.deliveryTimeGMTOffsetField; }
        set { this.deliveryTimeGMTOffsetField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal DimensionalWeight
    {
        get { return this.dimensionalWeightField; }
        set { this.dimensionalWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DimensionalWeightSpecified
    {
        get { return this.dimensionalWeightFieldSpecified; }
        set { this.dimensionalWeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PickupDayOfWeekNum
    {
        get { return this.pickupDayOfWeekNumField; }
        set { this.pickupDayOfWeekNumField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DestinationDayOfWeekNum
    {
        get { return this.destinationDayOfWeekNumField; }
        set { this.destinationDayOfWeekNumField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdShpExChrg", Form = System.Xml.Schema.XmlSchemaForm.Unqualified
        )]
    public QtdShpExChrgType1[] QtdShpExChrg
    {
        get { return this.qtdShpExChrgField; }
        set { this.qtdShpExChrgField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime PricingDate
    {
        get { return this.pricingDateField; }
        set { this.pricingDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PricingDateSpecified
    {
        get { return this.pricingDateFieldSpecified; }
        set { this.pricingDateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ShippingCharge
    {
        get { return this.shippingChargeField; }
        set { this.shippingChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShippingChargeSpecified
    {
        get { return this.shippingChargeFieldSpecified; }
        set { this.shippingChargeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TotalTaxAmount
    {
        get { return this.totalTaxAmountField; }
        set { this.totalTaxAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TotalTaxAmountSpecified
    {
        get { return this.totalTaxAmountFieldSpecified; }
        set { this.totalTaxAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdSInAdCur", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public QtdSInAdCurType[] QtdSInAdCur
    {
        get { return this.qtdSInAdCurField; }
        set { this.qtdSInAdCurField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("WeightChargeTaxDet",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightChargeTaxDetType[] WeightChargeTaxDet
    {
        get { return this.weightChargeTaxDetField; }
        set { this.weightChargeTaxDetField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "QtdShpExChrgType",
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class QtdShpExChrgType1
{

    private string specialServiceTypeField;

    private string localServiceTypeField;

    private string globalServiceNameField;

    private string localServiceTypeNameField;

    private string sOfferedCustAgreementField;

    private string chargeCodeTypeField;

    private decimal insPrmRateInPercentageField;

    private bool insPrmRateInPercentageFieldSpecified;

    private string currencyCodeField;

    private decimal chargeValueField;

    private bool chargeValueFieldSpecified;

    private decimal chargeTaxAmountField;

    private bool chargeTaxAmountFieldSpecified;

    private decimal[] chargeTaxRateField;

    private ChargeTaxAmountDetType[] chargeTaxAmountDetField;

    private QtdSExtrChrgInAdCurType[] qtdSExtrChrgInAdCurField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialServiceType
    {
        get { return this.specialServiceTypeField; }
        set { this.specialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalServiceType
    {
        get { return this.localServiceTypeField; }
        set { this.localServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalServiceName
    {
        get { return this.globalServiceNameField; }
        set { this.globalServiceNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalServiceTypeName
    {
        get { return this.localServiceTypeNameField; }
        set { this.localServiceTypeNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SOfferedCustAgreement
    {
        get { return this.sOfferedCustAgreementField; }
        set { this.sOfferedCustAgreementField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ChargeCodeType
    {
        get { return this.chargeCodeTypeField; }
        set { this.chargeCodeTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal InsPrmRateInPercentage
    {
        get { return this.insPrmRateInPercentageField; }
        set { this.insPrmRateInPercentageField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool InsPrmRateInPercentageSpecified
    {
        get { return this.insPrmRateInPercentageFieldSpecified; }
        set { this.insPrmRateInPercentageFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeValue
    {
        get { return this.chargeValueField; }
        set { this.chargeValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeValueSpecified
    {
        get { return this.chargeValueFieldSpecified; }
        set { this.chargeValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeTaxAmount
    {
        get { return this.chargeTaxAmountField; }
        set { this.chargeTaxAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeTaxAmountSpecified
    {
        get { return this.chargeTaxAmountFieldSpecified; }
        set { this.chargeTaxAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ChargeTaxRate",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal[] ChargeTaxRate
    {
        get { return this.chargeTaxRateField; }
        set { this.chargeTaxRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ChargeTaxAmountDet",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChargeTaxAmountDetType[] ChargeTaxAmountDet
    {
        get { return this.chargeTaxAmountDetField; }
        set { this.chargeTaxAmountDetField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdSExtrChrgInAdCur",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public QtdSExtrChrgInAdCurType[] QtdSExtrChrgInAdCur
    {
        get { return this.qtdSExtrChrgInAdCurField; }
        set { this.qtdSExtrChrgInAdCurField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class ChargeTaxAmountDetType
{

    private decimal taxTypeRateField;

    private bool taxTypeRateFieldSpecified;

    private string taxTypeCodeField;

    private decimal taxAmountField;

    private bool taxAmountFieldSpecified;

    private decimal baseAmountField;

    private bool baseAmountFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TaxTypeRate
    {
        get { return this.taxTypeRateField; }
        set { this.taxTypeRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TaxTypeRateSpecified
    {
        get { return this.taxTypeRateFieldSpecified; }
        set { this.taxTypeRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TaxTypeCode
    {
        get { return this.taxTypeCodeField; }
        set { this.taxTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TaxAmount
    {
        get { return this.taxAmountField; }
        set { this.taxAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TaxAmountSpecified
    {
        get { return this.taxAmountFieldSpecified; }
        set { this.taxAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal BaseAmount
    {
        get { return this.baseAmountField; }
        set { this.baseAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool BaseAmountSpecified
    {
        get { return this.baseAmountFieldSpecified; }
        set { this.baseAmountFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class QtdSExtrChrgInAdCurType
{

    private decimal chargeValueField;

    private bool chargeValueFieldSpecified;

    private decimal chargeExchangeRateField;

    private bool chargeExchangeRateFieldSpecified;

    private decimal chargeTaxAmountField;

    private bool chargeTaxAmountFieldSpecified;

    private string currencyCodeField;

    private string currencyRoleTypeCodeField;

    private ChargeTaxAmountDetType[] chargeTaxAmountDetField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeValue
    {
        get { return this.chargeValueField; }
        set { this.chargeValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeValueSpecified
    {
        get { return this.chargeValueFieldSpecified; }
        set { this.chargeValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeExchangeRate
    {
        get { return this.chargeExchangeRateField; }
        set { this.chargeExchangeRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeExchangeRateSpecified
    {
        get { return this.chargeExchangeRateFieldSpecified; }
        set { this.chargeExchangeRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeTaxAmount
    {
        get { return this.chargeTaxAmountField; }
        set { this.chargeTaxAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeTaxAmountSpecified
    {
        get { return this.chargeTaxAmountFieldSpecified; }
        set { this.chargeTaxAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyRoleTypeCode
    {
        get { return this.currencyRoleTypeCodeField; }
        set { this.currencyRoleTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ChargeTaxAmountDet",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ChargeTaxAmountDetType[] ChargeTaxAmountDet
    {
        get { return this.chargeTaxAmountDetField; }
        set { this.chargeTaxAmountDetField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class QtdSInAdCurType
{

    private decimal customsValueField;

    private bool customsValueFieldSpecified;

    private decimal exchangeRateField;

    private bool exchangeRateFieldSpecified;

    private string currencyCodeField;

    private string currencyRoleTypeCodeField;

    private decimal weightChargeField;

    private bool weightChargeFieldSpecified;

    private decimal totalAmountField;

    private bool totalAmountFieldSpecified;

    private decimal totalTaxAmountField;

    private bool totalTaxAmountFieldSpecified;

    private decimal weightChargeTaxField;

    private bool weightChargeTaxFieldSpecified;

    private WeightChargeTaxDetType[] weightChargeTaxDetField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal CustomsValue
    {
        get { return this.customsValueField; }
        set { this.customsValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CustomsValueSpecified
    {
        get { return this.customsValueFieldSpecified; }
        set { this.customsValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ExchangeRate
    {
        get { return this.exchangeRateField; }
        set { this.exchangeRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ExchangeRateSpecified
    {
        get { return this.exchangeRateFieldSpecified; }
        set { this.exchangeRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyRoleTypeCode
    {
        get { return this.currencyRoleTypeCodeField; }
        set { this.currencyRoleTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal WeightCharge
    {
        get { return this.weightChargeField; }
        set { this.weightChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightChargeSpecified
    {
        get { return this.weightChargeFieldSpecified; }
        set { this.weightChargeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TotalAmount
    {
        get { return this.totalAmountField; }
        set { this.totalAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TotalAmountSpecified
    {
        get { return this.totalAmountFieldSpecified; }
        set { this.totalAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TotalTaxAmount
    {
        get { return this.totalTaxAmountField; }
        set { this.totalTaxAmountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TotalTaxAmountSpecified
    {
        get { return this.totalTaxAmountFieldSpecified; }
        set { this.totalTaxAmountFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal WeightChargeTax
    {
        get { return this.weightChargeTaxField; }
        set { this.weightChargeTaxField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightChargeTaxSpecified
    {
        get { return this.weightChargeTaxFieldSpecified; }
        set { this.weightChargeTaxFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("WeightChargeTaxDet",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightChargeTaxDetType[] WeightChargeTaxDet
    {
        get { return this.weightChargeTaxDetField; }
        set { this.weightChargeTaxDetField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class WeightChargeTaxDetType
{

    private decimal taxTypeRateField;

    private bool taxTypeRateFieldSpecified;

    private string taxTypeCodeField;

    private decimal weightChargeTaxField;

    private bool weightChargeTaxFieldSpecified;

    private decimal baseAmtField;

    private bool baseAmtFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal TaxTypeRate
    {
        get { return this.taxTypeRateField; }
        set { this.taxTypeRateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TaxTypeRateSpecified
    {
        get { return this.taxTypeRateFieldSpecified; }
        set { this.taxTypeRateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TaxTypeCode
    {
        get { return this.taxTypeCodeField; }
        set { this.taxTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal WeightChargeTax
    {
        get { return this.weightChargeTaxField; }
        set { this.weightChargeTaxField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightChargeTaxSpecified
    {
        get { return this.weightChargeTaxFieldSpecified; }
        set { this.weightChargeTaxFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal BaseAmt
    {
        get { return this.baseAmtField; }
        set { this.baseAmtField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool BaseAmtSpecified
    {
        get { return this.baseAmtFieldSpecified; }
        set { this.baseAmtFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class SrvType
{

    private string globalProductCodeField;

    private MrkSrvType[] mrkSrvField;

    private SBTPType sBTPField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("MrkSrv", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public MrkSrvType[] MrkSrv
    {
        get { return this.mrkSrvField; }
        set { this.mrkSrvField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SBTPType SBTP
    {
        get { return this.sBTPField; }
        set { this.sBTPField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class MrkSrvType
{

    private string itemField;

    private ItemChoiceType itemElementNameField;

    private string item1Field;

    private Item1ChoiceType item1ElementNameField;

    private string item2Field;

    private Item2ChoiceType item2ElementNameField;

    private string item3Field;

    private Item3ChoiceType item3ElementNameField;

    private string networkTypeCodeField;

    private string item4Field;

    private Item4ChoiceType item4ElementNameField;

    private string transIndField;

    private string[] chargeCodeTypeField;

    private string mrkSrvIndField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("LocalProductCode", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("LocalServiceType", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
    public string Item
    {
        get { return this.itemField; }
        set { this.itemField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemChoiceType ItemElementName
    {
        get { return this.itemElementNameField; }
        set { this.itemElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GlobalServiceName", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("ProductShortName", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("Item1ElementName")]
    public string Item1
    {
        get { return this.item1Field; }
        set { this.item1Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public Item1ChoiceType Item1ElementName
    {
        get { return this.item1ElementNameField; }
        set { this.item1ElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("LocalProductName", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("LocalServiceTypeName", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("Item2ElementName")]
    public string Item2
    {
        get { return this.item2Field; }
        set { this.item2Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public Item2ChoiceType Item2ElementName
    {
        get { return this.item2ElementNameField; }
        set { this.item2ElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ProductDesc", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("ServiceDesc", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("Item3ElementName")]
    public string Item3
    {
        get { return this.item3Field; }
        set { this.item3Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public Item3ChoiceType Item3ElementName
    {
        get { return this.item3ElementNameField; }
        set { this.item3ElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string NetworkTypeCode
    {
        get { return this.networkTypeCodeField; }
        set { this.networkTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("POfferedCustAgreement", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("SOfferedCustAgreement", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("Item4ElementName")]
    public string Item4
    {
        get { return this.item4Field; }
        set { this.item4Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public Item4ChoiceType Item4ElementName
    {
        get { return this.item4ElementNameField; }
        set { this.item4ElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string TransInd
    {
        get { return this.transIndField; }
        set { this.transIndField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ChargeCodeType",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] ChargeCodeType
    {
        get { return this.chargeCodeTypeField; }
        set { this.chargeCodeTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MrkSrvInd
    {
        get { return this.mrkSrvIndField; }
        set { this.mrkSrvIndField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes",
    IncludeInSchema = false)]
public enum ItemChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":LocalProductCode")]
    LocalProductCode,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":LocalServiceType")]
    LocalServiceType,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes",
    IncludeInSchema = false)]
public enum Item1ChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":GlobalServiceName")]
    GlobalServiceName,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":ProductShortName")]
    ProductShortName,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes",
    IncludeInSchema = false)]
public enum Item2ChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":LocalProductName")]
    LocalProductName,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":LocalServiceTypeName")]
    LocalServiceTypeName,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes",
    IncludeInSchema = false)]
public enum Item3ChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":ProductDesc")]
    ProductDesc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":ServiceDesc")]
    ServiceDesc,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes",
    IncludeInSchema = false)]
public enum Item4ChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":POfferedCustAgreement")]
    POfferedCustAgreement,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":SOfferedCustAgreement")]
    SOfferedCustAgreement,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class SBTPType
{

    private ProdTypeVldSrvComb[] prodField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("VldSrvComb", Form = System.Xml.Schema.XmlSchemaForm.Unqualified
        , IsNullable = false)]
    public ProdTypeVldSrvComb[] Prod
    {
        get { return this.prodField; }
        set { this.prodField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class ProdTypeVldSrvComb
{

    private string specialServiceTypeField;

    private string[] localServiceTypeField;

    private ProdTypeVldSrvCombCombRSrv[] combRSrvField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialServiceType
    {
        get { return this.specialServiceTypeField; }
        set { this.specialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("LocalServiceType",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] LocalServiceType
    {
        get { return this.localServiceTypeField; }
        set { this.localServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CombRSrv", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ProdTypeVldSrvCombCombRSrv[] CombRSrv
    {
        get { return this.combRSrvField; }
        set { this.combRSrvField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class ProdTypeVldSrvCombCombRSrv
{

    private string restrictedSpecialServiceTypeField;

    private string[] restrictedLocalServiceTypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RestrictedSpecialServiceType
    {
        get { return this.restrictedSpecialServiceTypeField; }
        set { this.restrictedSpecialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("RestrictedLocalServiceType",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] RestrictedLocalServiceType
    {
        get { return this.restrictedLocalServiceTypeField; }
        set { this.restrictedLocalServiceTypeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
    Namespace = "http://www.dhl.com/DCTResponsedatatypes")]
public partial class NoteTypeCondition
{

    private string conditionCodeField;

    private string conditionDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionCode
    {
        get { return this.conditionCodeField; }
        set { this.conditionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionData
    {
        get { return this.conditionDataField; }
        set { this.conditionDataField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTResponseGetQuoteResponse
{

    private DCTResponseGetQuoteResponseResponse responseField;

    private BkgDetailsType2[] bkgDetailsField;

    private SrvType[] srvsField;

    private NoteTypeCondition[][] noteField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DCTResponseGetQuoteResponseResponse Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("BkgDetails", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BkgDetailsType2[] BkgDetails
    {
        get { return this.bkgDetailsField; }
        set { this.bkgDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Srv", Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public SrvType[] Srvs
    {
        get { return this.srvsField; }
        set { this.srvsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Condition", typeof(NoteTypeCondition),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public NoteTypeCondition[][] Note
    {
        get { return this.noteField; }
        set { this.noteField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DCTResponseGetQuoteResponseResponse
{

    private ServiceHeader2 serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader2 ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute("ErrorResponse", Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class ErrorResponse2
{

    private ErrorResponse responseField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ErrorResponse Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ErrorResponse
{

    private ServiceHeader1 serviceHeaderField;

    private Status statusField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader1 ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Status Status
    {
        get { return this.statusField; }
        set { this.statusField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Status
{

    private string actionStatusField;

    private Condition[] conditionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActionStatus
    {
        get { return this.actionStatusField; }
        set { this.actionStatusField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Condition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Condition[] Condition
    {
        get { return this.conditionField; }
        set { this.conditionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Condition
{

    private string conditionCodeField;

    private string conditionDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionCode
    {
        get { return this.conditionCodeField; }
        set { this.conditionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionData
    {
        get { return this.conditionDataField; }
        set { this.conditionDataField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class PickupErrorResponse
{

    private ErrorResponse1 responseField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ErrorResponse1 Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ErrorResponse", Namespace = "http://www.dhl.com/datatypes_EA"
    )]
public partial class ErrorResponse1
{

    private ServiceHeader serviceHeaderField;

    private Status1 statusField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Status1 Status
    {
        get { return this.statusField; }
        set { this.statusField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Status", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Status1
{

    private string actionStatusField;

    private Condition1[] conditionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActionStatus
    {
        get { return this.actionStatusField; }
        set { this.actionStatusField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Condition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Condition1[] Condition
    {
        get { return this.conditionField; }
        set { this.conditionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Condition", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Condition1
{

    private string conditionCodeField;

    private string conditionDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionCode
    {
        get { return this.conditionCodeField; }
        set { this.conditionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionData
    {
        get { return this.conditionDataField; }
        set { this.conditionDataField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class PickupResponse
{

    private Response1 responseField;

    private Note1 noteField;

    private string confirmationNumberField;

    private string readyByTimeField;

    private string secondReadyByTimeField;

    private System.DateTime nextPickupDateField;

    private bool nextPickupDateFieldSpecified;

    private string pickupChargeField;

    private string currencyCodeField;

    private string callInTimeField;

    private string secondCallInTimeField;

    private string originSvcAreaField;

    private string countryCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Response1 Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Note1 Note
    {
        get { return this.noteField; }
        set { this.noteField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ConfirmationNumber
    {
        get { return this.confirmationNumberField; }
        set { this.confirmationNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReadyByTime
    {
        get { return this.readyByTimeField; }
        set { this.readyByTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SecondReadyByTime
    {
        get { return this.secondReadyByTimeField; }
        set { this.secondReadyByTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime NextPickupDate
    {
        get { return this.nextPickupDateField; }
        set { this.nextPickupDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool NextPickupDateSpecified
    {
        get { return this.nextPickupDateFieldSpecified; }
        set { this.nextPickupDateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PickupCharge
    {
        get { return this.pickupChargeField; }
        set { this.pickupChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CallInTime
    {
        get { return this.callInTimeField; }
        set { this.callInTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SecondCallInTime
    {
        get { return this.secondCallInTimeField; }
        set { this.secondCallInTimeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OriginSvcArea
    {
        get { return this.originSvcAreaField; }
        set { this.originSvcAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Response", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Response1
{

    private ServiceHeader serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Note", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Note1
{

    private string actionNoteField;

    private Condition1[] conditionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActionNote
    {
        get { return this.actionNoteField; }
        set { this.actionNoteField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Condition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Condition1[] Condition
    {
        get { return this.conditionField; }
        set { this.conditionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class RoutingErrorResponse
{

    private ErrorResponse responseField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ErrorResponse Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class RoutingRequestEA
{

    private Request requestField;

    private RoutingRequestEARequestType requestTypeField;

    private string address1Field;

    private string address2Field;

    private string address3Field;

    private string postalCodeField;

    private string cityField;

    private string divisionField;

    private string countryCodeField;

    private string countryNameField;

    private string originCountryCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public RoutingRequestEARequestType RequestType
    {
        get { return this.requestTypeField; }
        set { this.requestTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address1
    {
        get { return this.address1Field; }
        set { this.address1Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address2
    {
        get { return this.address2Field; }
        set { this.address2Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Address3
    {
        get { return this.address3Field; }
        set { this.address3Field = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryName
    {
        get { return this.countryNameField; }
        set { this.countryNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OriginCountryCode
    {
        get { return this.originCountryCodeField; }
        set { this.originCountryCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
public enum RoutingRequestEARequestType
{

    /// <remarks/>
    O,

    /// <remarks/>
    D,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class RoutingResponse
{

    private Response responseField;

    private string gMTNegativeIndicatorField;

    private string gMTOffsetField;

    private string regionCodeField;

    private ServiceArea serviceAreaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Response Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GMTNegativeIndicator
    {
        get { return this.gMTNegativeIndicatorField; }
        set { this.gMTNegativeIndicatorField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GMTOffset
    {
        get { return this.gMTOffsetField; }
        set { this.gMTOffsetField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RegionCode
    {
        get { return this.regionCodeField; }
        set { this.regionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceArea ServiceArea
    {
        get { return this.serviceAreaField; }
        set { this.serviceAreaField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Response
{

    private ServiceHeader1 serviceHeaderField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceHeader1 ServiceHeader
    {
        get { return this.serviceHeaderField; }
        set { this.serviceHeaderField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ServiceArea
{

    private string serviceAreaCodeField;

    private string descriptionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ServiceAreaCode
    {
        get { return this.serviceAreaCodeField; }
        set { this.serviceAreaCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class ShipmentValidateErrorResponse
{

    private ErrorResponse responseField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ErrorResponse Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class ShipmentValidateRequestEA
{

    private Request requestField;

    private YesNo1 newShipperField;

    private bool newShipperFieldSpecified;

    private string languageCodeField;

    private PiecesEnabled piecesEnabledField;

    private bool piecesEnabledFieldSpecified;

    private Billing1 billingField;

    private Consignee1 consigneeField;

    private Commodity1[] commodityField;

    private Dutiable1 dutiableField;

    private ExportDeclaration1 exportDeclarationField;

    private Reference1[] referenceField;

    private ShipmentDetails2 shipmentDetailsField;

    private Shipper1 shipperField;

    private SpecialService1[] specialServiceField;

    private Place2 placeField;

    private YesNo1 eProcShipField;

    private bool eProcShipFieldSpecified;

    private DocImage1[] docImagesField;

    private LabelImageFormat labelImageFormatField;

    private bool labelImageFormatFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo1 NewShipper
    {
        get { return this.newShipperField; }
        set { this.newShipperField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool NewShipperSpecified
    {
        get { return this.newShipperFieldSpecified; }
        set { this.newShipperFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LanguageCode
    {
        get { return this.languageCodeField; }
        set { this.languageCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PiecesEnabled PiecesEnabled
    {
        get { return this.piecesEnabledField; }
        set { this.piecesEnabledField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PiecesEnabledSpecified
    {
        get { return this.piecesEnabledFieldSpecified; }
        set { this.piecesEnabledFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Billing1 Billing
    {
        get { return this.billingField; }
        set { this.billingField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Consignee1 Consignee
    {
        get { return this.consigneeField; }
        set { this.consigneeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Commodity", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Commodity1[] Commodity
    {
        get { return this.commodityField; }
        set { this.commodityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Dutiable1 Dutiable
    {
        get { return this.dutiableField; }
        set { this.dutiableField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportDeclaration1 ExportDeclaration
    {
        get { return this.exportDeclarationField; }
        set { this.exportDeclarationField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Reference", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Reference1[] Reference
    {
        get { return this.referenceField; }
        set { this.referenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentDetails2 ShipmentDetails
    {
        get { return this.shipmentDetailsField; }
        set { this.shipmentDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Shipper1 Shipper
    {
        get { return this.shipperField; }
        set { this.shipperField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SpecialService",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SpecialService1[] SpecialService
    {
        get { return this.specialServiceField; }
        set { this.specialServiceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Place2 Place
    {
        get { return this.placeField; }
        set { this.placeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo1 EProcShip
    {
        get { return this.eProcShipField; }
        set { this.eProcShipField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool EProcShipSpecified
    {
        get { return this.eProcShipFieldSpecified; }
        set { this.eProcShipFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public DocImage1[] DocImages
    {
        get { return this.docImagesField; }
        set { this.docImagesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LabelImageFormat LabelImageFormat
    {
        get { return this.labelImageFormatField; }
        set { this.labelImageFormatField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool LabelImageFormatSpecified
    {
        get { return this.labelImageFormatFieldSpecified; }
        set { this.labelImageFormatFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "YesNo", Namespace = "http://www.dhl.com/datatypes_EA")]
public enum YesNo1
{

    /// <remarks/>
    Y,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum PiecesEnabled
{

    /// <remarks/>
    Y,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Billing", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Billing1
{

    private string shipperAccountNumberField;

    private ShipmentPaymentType shippingPaymentTypeField;

    private string billingAccountNumberField;

    private DutyTaxPaymentType1 dutyPaymentTypeField;

    private bool dutyPaymentTypeFieldSpecified;

    private string dutyAccountNumberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ShipperAccountNumber
    {
        get { return this.shipperAccountNumberField; }
        set { this.shipperAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentPaymentType ShippingPaymentType
    {
        get { return this.shippingPaymentTypeField; }
        set { this.shippingPaymentTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string BillingAccountNumber
    {
        get { return this.billingAccountNumberField; }
        set { this.billingAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DutyTaxPaymentType1 DutyPaymentType
    {
        get { return this.dutyPaymentTypeField; }
        set { this.dutyPaymentTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DutyPaymentTypeSpecified
    {
        get { return this.dutyPaymentTypeFieldSpecified; }
        set { this.dutyPaymentTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string DutyAccountNumber
    {
        get { return this.dutyAccountNumberField; }
        set { this.dutyAccountNumberField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum ShipmentPaymentType
{

    /// <remarks/>
    S,

    /// <remarks/>
    R,

    /// <remarks/>
    T,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "DutyTaxPaymentType",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public enum DutyTaxPaymentType1
{

    /// <remarks/>
    S,

    /// <remarks/>
    R,

    /// <remarks/>
    T,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Consignee", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Consignee1
{

    private string companyNameField;

    private string[] addressLineField;

    private string cityField;

    private string divisionField;

    private string postalCodeField;

    private string countryCodeField;

    private string countryNameField;

    private string federalTaxIdField;

    private Contact2 contactField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public string[] AddressLine
    {
        get { return this.addressLineField; }
        set { this.addressLineField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryName
    {
        get { return this.countryNameField; }
        set { this.countryNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FederalTaxId
    {
        get { return this.federalTaxIdField; }
        set { this.federalTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Contact2 Contact
    {
        get { return this.contactField; }
        set { this.contactField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Contact", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Contact2
{

    private string personNameField;

    private string phoneNumberField;

    private string phoneExtensionField;

    private string faxNumberField;

    private string telexField;

    private Email1 emailField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PersonName
    {
        get { return this.personNameField; }
        set { this.personNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneNumber
    {
        get { return this.phoneNumberField; }
        set { this.phoneNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneExtension
    {
        get { return this.phoneExtensionField; }
        set { this.phoneExtensionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FaxNumber
    {
        get { return this.faxNumberField; }
        set { this.faxNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Telex
    {
        get { return this.telexField; }
        set { this.telexField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Email1 Email
    {
        get { return this.emailField; }
        set { this.emailField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Email", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Email1
{

    private string fromField;

    private string toField;

    private string[] ccField;

    private string subjectField;

    private string replyToField;

    private string bodyField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string From
    {
        get { return this.fromField; }
        set { this.fromField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string To
    {
        get { return this.toField; }
        set { this.toField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("cc", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] cc
    {
        get { return this.ccField; }
        set { this.ccField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Subject
    {
        get { return this.subjectField; }
        set { this.subjectField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReplyTo
    {
        get { return this.replyToField; }
        set { this.replyToField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Body
    {
        get { return this.bodyField; }
        set { this.bodyField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Commodity", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Commodity1
{

    private string commodityCodeField;

    private string commodityNameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CommodityCode
    {
        get { return this.commodityCodeField; }
        set { this.commodityCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CommodityName
    {
        get { return this.commodityNameField; }
        set { this.commodityNameField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Dutiable", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Dutiable1
{

    private string declaredValueField;

    private string declaredCurrencyField;

    private string shipperEINField;

    private string consigneeEINField;

    private object termsOfTradeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeclaredValue
    {
        get { return this.declaredValueField; }
        set { this.declaredValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeclaredCurrency
    {
        get { return this.declaredCurrencyField; }
        set { this.declaredCurrencyField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipperEIN
    {
        get { return this.shipperEINField; }
        set { this.shipperEINField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConsigneeEIN
    {
        get { return this.consigneeEINField; }
        set { this.consigneeEINField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object TermsOfTrade
    {
        get { return this.termsOfTradeField; }
        set { this.termsOfTradeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ExportDeclaration",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class ExportDeclaration1
{

    private string interConsigneeField;

    private YesNo1 isPartiesRelationField;

    private bool isPartiesRelationFieldSpecified;

    private string signatureNameField;

    private string signatureTitleField;

    private string exportReasonField;

    private ExportReasonCode1 exportReasonCodeField;

    private bool exportReasonCodeFieldSpecified;

    private ExportLineItem1[] exportLineItemField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InterConsignee
    {
        get { return this.interConsigneeField; }
        set { this.interConsigneeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo1 IsPartiesRelation
    {
        get { return this.isPartiesRelationField; }
        set { this.isPartiesRelationField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsPartiesRelationSpecified
    {
        get { return this.isPartiesRelationFieldSpecified; }
        set { this.isPartiesRelationFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SignatureName
    {
        get { return this.signatureNameField; }
        set { this.signatureNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SignatureTitle
    {
        get { return this.signatureTitleField; }
        set { this.signatureTitleField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ExportReason
    {
        get { return this.exportReasonField; }
        set { this.exportReasonField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportReasonCode1 ExportReasonCode
    {
        get { return this.exportReasonCodeField; }
        set { this.exportReasonCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ExportReasonCodeSpecified
    {
        get { return this.exportReasonCodeFieldSpecified; }
        set { this.exportReasonCodeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ExportLineItem",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportLineItem1[] ExportLineItem
    {
        get { return this.exportLineItemField; }
        set { this.exportLineItemField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ExportReasonCode",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public enum ExportReasonCode1
{

    /// <remarks/>
    P,

    /// <remarks/>
    T,

    /// <remarks/>
    R,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ExportLineItem",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class ExportLineItem1
{

    private string lineNumberField;

    private string quantityField;

    private string quantityUnitField;

    private string descriptionField;

    private float valueField;

    private YesNo1 isDomesticField;

    private bool isDomesticFieldSpecified;

    private string commodityCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string LineNumber
    {
        get { return this.lineNumberField; }
        set { this.lineNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Quantity
    {
        get { return this.quantityField; }
        set { this.quantityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string QuantityUnit
    {
        get { return this.quantityUnitField; }
        set { this.quantityUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float Value
    {
        get { return this.valueField; }
        set { this.valueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo1 IsDomestic
    {
        get { return this.isDomesticField; }
        set { this.isDomesticField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsDomesticSpecified
    {
        get { return this.isDomesticFieldSpecified; }
        set { this.isDomesticFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CommodityCode
    {
        get { return this.commodityCodeField; }
        set { this.commodityCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Reference", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Reference1
{

    private string referenceIDField;

    private string referenceTypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenceID
    {
        get { return this.referenceIDField; }
        set { this.referenceIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenceType
    {
        get { return this.referenceTypeField; }
        set { this.referenceTypeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ShipmentDetails",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class ShipmentDetails2
{

    private string numberOfPiecesField;

    private string currencyCodeField;

    private Piece2[] piecesField;

    private PackageType1 packageTypeField;

    private decimal weightField;

    private DimensionalUnit dimensionUnitField;

    private bool dimensionUnitFieldSpecified;

    private WeightUnit weightUnitField;

    private string globalProductCodeField;

    private string localProductCodeField;

    private DoorTo doorToField;

    private bool doorToFieldSpecified;

    private System.DateTime dateField;

    private string contentsField;

    private YesNo1 isDutiableField;

    private bool isDutiableFieldSpecified;

    private string insuredAmountField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string NumberOfPieces
    {
        get { return this.numberOfPiecesField; }
        set { this.numberOfPiecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public Piece2[] Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PackageType1 PackageType
    {
        get { return this.packageTypeField; }
        set { this.packageTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DimensionalUnit DimensionUnit
    {
        get { return this.dimensionUnitField; }
        set { this.dimensionUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DimensionUnitSpecified
    {
        get { return this.dimensionUnitFieldSpecified; }
        set { this.dimensionUnitFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightUnit WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LocalProductCode
    {
        get { return this.localProductCodeField; }
        set { this.localProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DoorTo DoorTo
    {
        get { return this.doorToField; }
        set { this.doorToField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DoorToSpecified
    {
        get { return this.doorToFieldSpecified; }
        set { this.doorToFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime Date
    {
        get { return this.dateField; }
        set { this.dateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Contents
    {
        get { return this.contentsField; }
        set { this.contentsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo1 IsDutiable
    {
        get { return this.isDutiableField; }
        set { this.isDutiableField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsDutiableSpecified
    {
        get { return this.isDutiableFieldSpecified; }
        set { this.isDutiableFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InsuredAmount
    {
        get { return this.insuredAmountField; }
        set { this.insuredAmountField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Piece", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Piece2
{

    private string pieceIDField;

    private PackageType1 packageTypeField;

    private bool packageTypeFieldSpecified;

    private decimal weightField;

    private bool weightFieldSpecified;

    private decimal dimWeightField;

    private bool dimWeightFieldSpecified;

    private string depthField;

    private string widthField;

    private string heightField;

    private string pieceContentsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceID
    {
        get { return this.pieceIDField; }
        set { this.pieceIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PackageType1 PackageType
    {
        get { return this.packageTypeField; }
        set { this.packageTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PackageTypeSpecified
    {
        get { return this.packageTypeFieldSpecified; }
        set { this.packageTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightSpecified
    {
        get { return this.weightFieldSpecified; }
        set { this.weightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal DimWeight
    {
        get { return this.dimWeightField; }
        set { this.dimWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DimWeightSpecified
    {
        get { return this.dimWeightFieldSpecified; }
        set { this.dimWeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Depth
    {
        get { return this.depthField; }
        set { this.depthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Width
    {
        get { return this.widthField; }
        set { this.widthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Height
    {
        get { return this.heightField; }
        set { this.heightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceContents
    {
        get { return this.pieceContentsField; }
        set { this.pieceContentsField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "PackageType", Namespace = "http://www.dhl.com/datatypes_EA")]
public enum PackageType1
{

    /// <remarks/>
    DF,

    /// <remarks/>
    EE,

    /// <remarks/>
    OD,

    /// <remarks/>
    CP,

    /// <remarks/>
    YP,

    /// <remarks/>
    JB,

    /// <remarks/>
    DC,

    /// <remarks/>
    DM,

    /// <remarks/>
    ED,

    /// <remarks/>
    FR,

    /// <remarks/>
    BD,

    /// <remarks/>
    BP,

    /// <remarks/>
    JD,

    /// <remarks/>
    JP,

    /// <remarks/>
    PA,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum DimensionalUnit
{

    /// <remarks/>
    C,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Shipper", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Shipper1
{

    private string shipperIDField;

    private string companyNameField;

    private string registeredAccountField;

    private string[] addressLineField;

    private string cityField;

    private string divisionField;

    private string postalCodeField;

    private string countryCodeField;

    private string countryNameField;

    private string federalTaxIdField;

    private Contact2 contactField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipperID
    {
        get { return this.shipperIDField; }
        set { this.shipperIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string RegisteredAccount
    {
        get { return this.registeredAccountField; }
        set { this.registeredAccountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public string[] AddressLine
    {
        get { return this.addressLineField; }
        set { this.addressLineField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryName
    {
        get { return this.countryNameField; }
        set { this.countryNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FederalTaxId
    {
        get { return this.federalTaxIdField; }
        set { this.federalTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Contact2 Contact
    {
        get { return this.contactField; }
        set { this.contactField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "SpecialService",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class SpecialService1
{

    private string specialServiceTypeField;

    private CommunicationType1 communicationTypeField;

    private bool communicationTypeFieldSpecified;

    private string chargeValueField;

    private string currencyCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialServiceType
    {
        get { return this.specialServiceTypeField; }
        set { this.specialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CommunicationType1 CommunicationType
    {
        get { return this.communicationTypeField; }
        set { this.communicationTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CommunicationTypeSpecified
    {
        get { return this.communicationTypeFieldSpecified; }
        set { this.communicationTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ChargeValue
    {
        get { return this.chargeValueField; }
        set { this.chargeValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "CommunicationType",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public enum CommunicationType1
{

    /// <remarks/>
    P,

    /// <remarks/>
    F,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Place", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class Place2
{

    private ResidenceOrBusiness1 residenceOrBusinessField;

    private bool residenceOrBusinessFieldSpecified;

    private string companyNameField;

    private string[] addressLineField;

    private string cityField;

    private string countryCodeField;

    private string divisionCodeField;

    private string divisionField;

    private string postalCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ResidenceOrBusiness1 ResidenceOrBusiness
    {
        get { return this.residenceOrBusinessField; }
        set { this.residenceOrBusinessField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ResidenceOrBusinessSpecified
    {
        get { return this.residenceOrBusinessFieldSpecified; }
        set { this.residenceOrBusinessFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public string[] AddressLine
    {
        get { return this.addressLineField; }
        set { this.addressLineField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DivisionCode
    {
        get { return this.divisionCodeField; }
        set { this.divisionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ResidenceOrBusiness",
    Namespace = "http://www.dhl.com/datatypes_EA")]
public enum ResidenceOrBusiness1
{

    /// <remarks/>
    B,

    /// <remarks/>
    R,

    /// <remarks/>
    C,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "DocImage", Namespace = "http://www.dhl.com/datatypes_EA")]
public partial class DocImage1
{

    private Type1 typeField;

    private byte[] imageField;

    private ImageFormat1 imageFormatField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Type1 Type
    {
        get { return this.typeField; }
        set { this.typeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] Image
    {
        get { return this.imageField; }
        set { this.imageField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ImageFormat1 ImageFormat
    {
        get { return this.imageFormatField; }
        set { this.imageFormatField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Type", Namespace = "http://www.dhl.com/datatypes_EA")]
public enum Type1
{

    /// <remarks/>
    HWB,

    /// <remarks/>
    INV,

    /// <remarks/>
    PNV,

    /// <remarks/>
    COO,

    /// <remarks/>
    NAF,

    /// <remarks/>
    CIN,

    /// <remarks/>
    DCL,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "ImageFormat", Namespace = "http://www.dhl.com/datatypes_EA")]
public enum ImageFormat1
{

    /// <remarks/>
    PDF,

    /// <remarks/>
    PNG,

    /// <remarks/>
    TIFF,

    /// <remarks/>
    GIF,

    /// <remarks/>
    JPEG,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes_EA")]
public enum LabelImageFormat
{

    /// <remarks/>
    EPL2,

    /// <remarks/>
    PDF,

    /// <remarks/>
    ZPL2,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class ShipmentValidateResponse
{

    private Response responseField;

    private Note noteField;

    private string airwayBillNumberField;

    private BillCode billingCodeField;

    private string currencyCodeField;

    private string courierMessageField;

    private string dHLRoutingCodeField;

    private string dHLRoutingDataIdField;

    private DestinationServiceArea destinationServiceAreaField;

    private OriginServiceArea originServiceAreaField;

    private string productContentCodeField;

    private string productShortNameField;

    private string[] internalServiceCodeField;

    private string deliveryDateCodeField;

    private string deliveryTimeCodeField;

    private ShipValResponsePiece[] piecesField;

    private float packageChargeField;

    private bool packageChargeFieldSpecified;

    private YesNo ratedField;

    private float shippingChargeField;

    private bool shippingChargeFieldSpecified;

    private float shippingChargeInUSDField;

    private bool shippingChargeInUSDFieldSpecified;

    private string weightUnitField;

    private decimal chargeableWeightField;

    private decimal dimensionalWeightField;

    private string countryCodeField;

    private ShipmentValidateResponseBarcodes barcodesField;

    private string pieceField;

    private string contentsField;

    private Reference[] referenceField;

    private Consignee consigneeField;

    private Shipper shipperField;

    private string customerIDField;

    private System.DateTime shipmentDateField;

    private string globalProductCodeField;

    private SpecialService[] specialServiceField;

    private Billing billingField;

    private Dutiable dutiableField;

    private ExportDeclaration exportDeclarationField;

    private YesNo newShipperField;

    private bool newShipperFieldSpecified;

    private PLTStatus pLTStatusField;

    private bool pLTStatusFieldSpecified;

    private QtdSInAdCur[] qtdSInAdCurField;

    private LabelImage[] labelImageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Response Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Note Note
    {
        get { return this.noteField; }
        set { this.noteField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string AirwayBillNumber
    {
        get { return this.airwayBillNumberField; }
        set { this.airwayBillNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public BillCode BillingCode
    {
        get { return this.billingCodeField; }
        set { this.billingCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CourierMessage
    {
        get { return this.courierMessageField; }
        set { this.courierMessageField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DHLRoutingCode
    {
        get { return this.dHLRoutingCodeField; }
        set { this.dHLRoutingCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DHLRoutingDataId
    {
        get { return this.dHLRoutingDataIdField; }
        set { this.dHLRoutingDataIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DestinationServiceArea DestinationServiceArea
    {
        get { return this.destinationServiceAreaField; }
        set { this.destinationServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OriginServiceArea OriginServiceArea
    {
        get { return this.originServiceAreaField; }
        set { this.originServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ProductContentCode
    {
        get { return this.productContentCodeField; }
        set { this.productContentCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ProductShortName
    {
        get { return this.productShortNameField; }
        set { this.productShortNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("InternalServiceCode",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] InternalServiceCode
    {
        get { return this.internalServiceCodeField; }
        set { this.internalServiceCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeliveryDateCode
    {
        get { return this.deliveryDateCodeField; }
        set { this.deliveryDateCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeliveryTimeCode
    {
        get { return this.deliveryTimeCodeField; }
        set { this.deliveryTimeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Piece", Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public ShipValResponsePiece[] Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float PackageCharge
    {
        get { return this.packageChargeField; }
        set { this.packageChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PackageChargeSpecified
    {
        get { return this.packageChargeFieldSpecified; }
        set { this.packageChargeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo Rated
    {
        get { return this.ratedField; }
        set { this.ratedField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float ShippingCharge
    {
        get { return this.shippingChargeField; }
        set { this.shippingChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShippingChargeSpecified
    {
        get { return this.shippingChargeFieldSpecified; }
        set { this.shippingChargeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float ShippingChargeInUSD
    {
        get { return this.shippingChargeInUSDField; }
        set { this.shippingChargeInUSDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShippingChargeInUSDSpecified
    {
        get { return this.shippingChargeInUSDFieldSpecified; }
        set { this.shippingChargeInUSDFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ChargeableWeight
    {
        get { return this.chargeableWeightField; }
        set { this.chargeableWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal DimensionalWeight
    {
        get { return this.dimensionalWeightField; }
        set { this.dimensionalWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentValidateResponseBarcodes Barcodes
    {
        get { return this.barcodesField; }
        set { this.barcodesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Piece
    {
        get { return this.pieceField; }
        set { this.pieceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Contents
    {
        get { return this.contentsField; }
        set { this.contentsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Reference", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Reference[] Reference
    {
        get { return this.referenceField; }
        set { this.referenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Consignee Consignee
    {
        get { return this.consigneeField; }
        set { this.consigneeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Shipper Shipper
    {
        get { return this.shipperField; }
        set { this.shipperField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CustomerID
    {
        get { return this.customerIDField; }
        set { this.customerIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime ShipmentDate
    {
        get { return this.shipmentDateField; }
        set { this.shipmentDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("SpecialService",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SpecialService[] SpecialService
    {
        get { return this.specialServiceField; }
        set { this.specialServiceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Billing Billing
    {
        get { return this.billingField; }
        set { this.billingField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Dutiable Dutiable
    {
        get { return this.dutiableField; }
        set { this.dutiableField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportDeclaration ExportDeclaration
    {
        get { return this.exportDeclarationField; }
        set { this.exportDeclarationField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo NewShipper
    {
        get { return this.newShipperField; }
        set { this.newShipperField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool NewShipperSpecified
    {
        get { return this.newShipperFieldSpecified; }
        set { this.newShipperFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PLTStatus PLTStatus
    {
        get { return this.pLTStatusField; }
        set { this.pLTStatusField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PLTStatusSpecified
    {
        get { return this.pLTStatusFieldSpecified; }
        set { this.pLTStatusFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("QtdSInAdCur", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public QtdSInAdCur[] QtdSInAdCur
    {
        get { return this.qtdSInAdCurField; }
        set { this.qtdSInAdCurField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("LabelImage", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LabelImage[] LabelImage
    {
        get { return this.labelImageField; }
        set { this.labelImageField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Note
{

    private string actionNoteField;

    private Condition[] conditionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActionNote
    {
        get { return this.actionNoteField; }
        set { this.actionNoteField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Condition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Condition[] Condition
    {
        get { return this.conditionField; }
        set { this.conditionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum BillCode
{

    /// <remarks/>
    DSA,

    /// <remarks/>
    DBA,

    /// <remarks/>
    TCA,

    /// <remarks/>
    IEA,

    /// <remarks/>
    UAN,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class DestinationServiceArea
{

    private string serviceAreaCodeField;

    private string descriptionField;

    private string facilityCodeField;

    private string inboundSortCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ServiceAreaCode
    {
        get { return this.serviceAreaCodeField; }
        set { this.serviceAreaCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FacilityCode
    {
        get { return this.facilityCodeField; }
        set { this.facilityCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InboundSortCode
    {
        get { return this.inboundSortCodeField; }
        set { this.inboundSortCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class OriginServiceArea
{

    private string serviceAreaCodeField;

    private string descriptionField;

    private string outboundSortCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ServiceAreaCode
    {
        get { return this.serviceAreaCodeField; }
        set { this.serviceAreaCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OutboundSortCode
    {
        get { return this.outboundSortCodeField; }
        set { this.outboundSortCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ShipValResponsePiece
{

    private string pieceNumberField;

    private string depthField;

    private string widthField;

    private string heightField;

    private decimal weightField;

    private bool weightFieldSpecified;

    private PackageType packageTypeField;

    private bool packageTypeFieldSpecified;

    private decimal dimWeightField;

    private bool dimWeightFieldSpecified;

    private string pieceContentsField;

    private string dataIdentifierField;

    private string licensePlateField;

    private byte[] licensePlateBarCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string PieceNumber
    {
        get { return this.pieceNumberField; }
        set { this.pieceNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Depth
    {
        get { return this.depthField; }
        set { this.depthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Width
    {
        get { return this.widthField; }
        set { this.widthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Height
    {
        get { return this.heightField; }
        set { this.heightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightSpecified
    {
        get { return this.weightFieldSpecified; }
        set { this.weightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PackageType PackageType
    {
        get { return this.packageTypeField; }
        set { this.packageTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PackageTypeSpecified
    {
        get { return this.packageTypeFieldSpecified; }
        set { this.packageTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal DimWeight
    {
        get { return this.dimWeightField; }
        set { this.dimWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DimWeightSpecified
    {
        get { return this.dimWeightFieldSpecified; }
        set { this.dimWeightFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceContents
    {
        get { return this.pieceContentsField; }
        set { this.pieceContentsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DataIdentifier
    {
        get { return this.dataIdentifierField; }
        set { this.dataIdentifierField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LicensePlate
    {
        get { return this.licensePlateField; }
        set { this.licensePlateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] LicensePlateBarCode
    {
        get { return this.licensePlateBarCodeField; }
        set { this.licensePlateBarCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum PackageType
{

    /// <remarks/>
    EE,

    /// <remarks/>
    OD,

    /// <remarks/>
    CP,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum YesNo
{

    /// <remarks/>
    Y,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
public partial class ShipmentValidateResponseBarcodes
{

    private byte[] aWBBarCodeField;

    private byte[] originDestnBarcodeField;

    private byte[] barCode2DField;

    private byte[] dHLRoutingBarCodeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] AWBBarCode
    {
        get { return this.aWBBarCodeField; }
        set { this.aWBBarCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] OriginDestnBarcode
    {
        get { return this.originDestnBarcodeField; }
        set { this.originDestnBarcodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] BarCode2D
    {
        get { return this.barCode2DField; }
        set { this.barCode2DField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] DHLRoutingBarCode
    {
        get { return this.dHLRoutingBarCodeField; }
        set { this.dHLRoutingBarCodeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Reference
{

    private string referenceIDField;

    private string referenceTypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenceID
    {
        get { return this.referenceIDField; }
        set { this.referenceIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReferenceType
    {
        get { return this.referenceTypeField; }
        set { this.referenceTypeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Consignee
{

    private string companyNameField;

    private string[] addressLineField;

    private string cityField;

    private string divisionField;

    private string divisionCodeField;

    private string postalCodeField;

    private string countryCodeField;

    private string countryNameField;

    private string federalTaxIdField;

    private string stateTaxIdField;

    private Contact1 contactField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public string[] AddressLine
    {
        get { return this.addressLineField; }
        set { this.addressLineField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DivisionCode
    {
        get { return this.divisionCodeField; }
        set { this.divisionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryName
    {
        get { return this.countryNameField; }
        set { this.countryNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FederalTaxId
    {
        get { return this.federalTaxIdField; }
        set { this.federalTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StateTaxId
    {
        get { return this.stateTaxIdField; }
        set { this.stateTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Contact1 Contact
    {
        get { return this.contactField; }
        set { this.contactField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "Contact", Namespace = "http://www.dhl.com/datatypes")]
public partial class Contact1
{

    private string personNameField;

    private string phoneNumberField;

    private string phoneExtensionField;

    private string faxNumberField;

    private string telexField;

    private Email emailField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PersonName
    {
        get { return this.personNameField; }
        set { this.personNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneNumber
    {
        get { return this.phoneNumberField; }
        set { this.phoneNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PhoneExtension
    {
        get { return this.phoneExtensionField; }
        set { this.phoneExtensionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FaxNumber
    {
        get { return this.faxNumberField; }
        set { this.faxNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Telex
    {
        get { return this.telexField; }
        set { this.telexField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Email Email
    {
        get { return this.emailField; }
        set { this.emailField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Email
{

    private string fromField;

    private string toField;

    private string[] ccField;

    private string subjectField;

    private string replyToField;

    private string bodyField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string From
    {
        get { return this.fromField; }
        set { this.fromField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string To
    {
        get { return this.toField; }
        set { this.toField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("cc", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string[] cc
    {
        get { return this.ccField; }
        set { this.ccField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Subject
    {
        get { return this.subjectField; }
        set { this.subjectField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ReplyTo
    {
        get { return this.replyToField; }
        set { this.replyToField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Body
    {
        get { return this.bodyField; }
        set { this.bodyField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Shipper
{

    private string shipperIDField;

    private string companyNameField;

    private string registeredAccountField;

    private string[] addressLineField;

    private string cityField;

    private string divisionField;

    private string divisionCodeField;

    private string postalCodeField;

    private string countryCodeField;

    private string countryNameField;

    private string federalTaxIdField;

    private string stateTaxIdField;

    private Contact1 contactField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipperID
    {
        get { return this.shipperIDField; }
        set { this.shipperIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CompanyName
    {
        get { return this.companyNameField; }
        set { this.companyNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string RegisteredAccount
    {
        get { return this.registeredAccountField; }
        set { this.registeredAccountField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AddressLine", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)
    ]
    public string[] AddressLine
    {
        get { return this.addressLineField; }
        set { this.addressLineField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string City
    {
        get { return this.cityField; }
        set { this.cityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Division
    {
        get { return this.divisionField; }
        set { this.divisionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DivisionCode
    {
        get { return this.divisionCodeField; }
        set { this.divisionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PostalCode
    {
        get { return this.postalCodeField; }
        set { this.postalCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryCode
    {
        get { return this.countryCodeField; }
        set { this.countryCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CountryName
    {
        get { return this.countryNameField; }
        set { this.countryNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string FederalTaxId
    {
        get { return this.federalTaxIdField; }
        set { this.federalTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StateTaxId
    {
        get { return this.stateTaxIdField; }
        set { this.stateTaxIdField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Contact1 Contact
    {
        get { return this.contactField; }
        set { this.contactField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class SpecialService
{

    private string specialServiceTypeField;

    private string communicationAddressField;

    private CommunicationType communicationTypeField;

    private bool communicationTypeFieldSpecified;

    private float chargeValueField;

    private bool chargeValueFieldSpecified;

    private string currencyCodeField;

    private YesNo isWaivedField;

    private bool isWaivedFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SpecialServiceType
    {
        get { return this.specialServiceTypeField; }
        set { this.specialServiceTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CommunicationAddress
    {
        get { return this.communicationAddressField; }
        set { this.communicationAddressField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CommunicationType CommunicationType
    {
        get { return this.communicationTypeField; }
        set { this.communicationTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CommunicationTypeSpecified
    {
        get { return this.communicationTypeFieldSpecified; }
        set { this.communicationTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float ChargeValue
    {
        get { return this.chargeValueField; }
        set { this.chargeValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ChargeValueSpecified
    {
        get { return this.chargeValueFieldSpecified; }
        set { this.chargeValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo IsWaived
    {
        get { return this.isWaivedField; }
        set { this.isWaivedField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsWaivedSpecified
    {
        get { return this.isWaivedFieldSpecified; }
        set { this.isWaivedFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum CommunicationType
{

    /// <remarks/>
    P,

    /// <remarks/>
    F,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Billing
{

    private string shipperAccountNumberField;

    private PaymentType shippingPaymentTypeField;

    private string billingAccountNumberField;

    private DutyTaxPaymentType dutyPaymentTypeField;

    private string dutyAccountNumberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ShipperAccountNumber
    {
        get { return this.shipperAccountNumberField; }
        set { this.shipperAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PaymentType ShippingPaymentType
    {
        get { return this.shippingPaymentTypeField; }
        set { this.shippingPaymentTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string BillingAccountNumber
    {
        get { return this.billingAccountNumberField; }
        set { this.billingAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DutyTaxPaymentType DutyPaymentType
    {
        get { return this.dutyPaymentTypeField; }
        set { this.dutyPaymentTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string DutyAccountNumber
    {
        get { return this.dutyAccountNumberField; }
        set { this.dutyAccountNumberField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum PaymentType
{

    /// <remarks/>
    S,

    /// <remarks/>
    R,

    /// <remarks/>
    T,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum DutyTaxPaymentType
{

    /// <remarks/>
    S,

    /// <remarks/>
    R,

    /// <remarks/>
    T,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class Dutiable
{

    private float declaredValueField;

    private bool declaredValueFieldSpecified;

    private string declaredCurrencyField;

    private string scheduleBField;

    private object exportLicenseField;

    private string shipperEINField;

    private DutiableShipperIDType shipperIDTypeField;

    private bool shipperIDTypeFieldSpecified;

    private DutiableConsigneeIDType consigneeIDTypeField;

    private bool consigneeIDTypeFieldSpecified;

    private object importLicenseField;

    private string consigneeEINField;

    private object termsOfTradeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float DeclaredValue
    {
        get { return this.declaredValueField; }
        set { this.declaredValueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DeclaredValueSpecified
    {
        get { return this.declaredValueFieldSpecified; }
        set { this.declaredValueFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DeclaredCurrency
    {
        get { return this.declaredCurrencyField; }
        set { this.declaredCurrencyField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ScheduleB
    {
        get { return this.scheduleBField; }
        set { this.scheduleBField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object ExportLicense
    {
        get { return this.exportLicenseField; }
        set { this.exportLicenseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipperEIN
    {
        get { return this.shipperEINField; }
        set { this.shipperEINField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DutiableShipperIDType ShipperIDType
    {
        get { return this.shipperIDTypeField; }
        set { this.shipperIDTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ShipperIDTypeSpecified
    {
        get { return this.shipperIDTypeFieldSpecified; }
        set { this.shipperIDTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DutiableConsigneeIDType ConsigneeIDType
    {
        get { return this.consigneeIDTypeField; }
        set { this.consigneeIDTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ConsigneeIDTypeSpecified
    {
        get { return this.consigneeIDTypeFieldSpecified; }
        set { this.consigneeIDTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object ImportLicense
    {
        get { return this.importLicenseField; }
        set { this.importLicenseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConsigneeEIN
    {
        get { return this.consigneeEINField; }
        set { this.consigneeEINField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object TermsOfTrade
    {
        get { return this.termsOfTradeField; }
        set { this.termsOfTradeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public enum DutiableShipperIDType
{

    /// <remarks/>
    S,

    /// <remarks/>
    E,

    /// <remarks/>
    D,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public enum DutiableConsigneeIDType
{

    /// <remarks/>
    S,

    /// <remarks/>
    E,

    /// <remarks/>
    D,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ExportDeclaration
{

    private string interConsigneeField;

    private YesNo isPartiesRelationField;

    private bool isPartiesRelationFieldSpecified;

    private string eCCNField;

    private string signatureNameField;

    private string signatureTitleField;

    private string exportReasonField;

    private ExportReasonCode exportReasonCodeField;

    private bool exportReasonCodeFieldSpecified;

    private SEDNumber sedNumberField;

    private bool sedNumberFieldSpecified;

    private SEDNumberType sedNumberTypeField;

    private bool sedNumberTypeFieldSpecified;

    private string mxStateCodeField;

    private ExportLineItem[] exportLineItemField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string InterConsignee
    {
        get { return this.interConsigneeField; }
        set { this.interConsigneeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo IsPartiesRelation
    {
        get { return this.isPartiesRelationField; }
        set { this.isPartiesRelationField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsPartiesRelationSpecified
    {
        get { return this.isPartiesRelationFieldSpecified; }
        set { this.isPartiesRelationFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ECCN
    {
        get { return this.eCCNField; }
        set { this.eCCNField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SignatureName
    {
        get { return this.signatureNameField; }
        set { this.signatureNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SignatureTitle
    {
        get { return this.signatureTitleField; }
        set { this.signatureTitleField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ExportReason
    {
        get { return this.exportReasonField; }
        set { this.exportReasonField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportReasonCode ExportReasonCode
    {
        get { return this.exportReasonCodeField; }
        set { this.exportReasonCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ExportReasonCodeSpecified
    {
        get { return this.exportReasonCodeFieldSpecified; }
        set { this.exportReasonCodeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SEDNumber SedNumber
    {
        get { return this.sedNumberField; }
        set { this.sedNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SedNumberSpecified
    {
        get { return this.sedNumberFieldSpecified; }
        set { this.sedNumberFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public SEDNumberType SedNumberType
    {
        get { return this.sedNumberTypeField; }
        set { this.sedNumberTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SedNumberTypeSpecified
    {
        get { return this.sedNumberTypeFieldSpecified; }
        set { this.sedNumberTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string MxStateCode
    {
        get { return this.mxStateCodeField; }
        set { this.mxStateCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ExportLineItem",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportLineItem[] ExportLineItem
    {
        get { return this.exportLineItemField; }
        set { this.exportLineItemField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum ExportReasonCode
{

    /// <remarks/>
    P,

    /// <remarks/>
    T,

    /// <remarks/>
    R,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum SEDNumber
{

    /// <remarks/>
    FTSR,

    /// <remarks/>
    XTN,

    /// <remarks/>
    SAS,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum SEDNumberType
{

    /// <remarks/>
    F,

    /// <remarks/>
    X,

    /// <remarks/>
    S,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ExportLineItem
{

    private string lineNumberField;

    private string quantityField;

    private string quantityUnitField;

    private string descriptionField;

    private float valueField;

    private YesNo isDomesticField;

    private bool isDomesticFieldSpecified;

    private string commodityCodeField;

    private string scheduleBField;

    private string eCCNField;

    private ExportLineItemWeight weightField;

    private ExportLineItemLicense licenseField;

    private string licenseSymbolField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string LineNumber
    {
        get { return this.lineNumberField; }
        set { this.lineNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string Quantity
    {
        get { return this.quantityField; }
        set { this.quantityField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string QuantityUnit
    {
        get { return this.quantityUnitField; }
        set { this.quantityUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float Value
    {
        get { return this.valueField; }
        set { this.valueField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public YesNo IsDomestic
    {
        get { return this.isDomesticField; }
        set { this.isDomesticField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool IsDomesticSpecified
    {
        get { return this.isDomesticFieldSpecified; }
        set { this.isDomesticFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CommodityCode
    {
        get { return this.commodityCodeField; }
        set { this.commodityCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ScheduleB
    {
        get { return this.scheduleBField; }
        set { this.scheduleBField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ECCN
    {
        get { return this.eCCNField; }
        set { this.eCCNField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportLineItemWeight Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ExportLineItemLicense License
    {
        get { return this.licenseField; }
        set { this.licenseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LicenseSymbol
    {
        get { return this.licenseSymbolField; }
        set { this.licenseSymbolField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public partial class ExportLineItemWeight
{

    private decimal weightField;

    private WeightUnit1 weightUnitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public WeightUnit1 WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "WeightUnit", Namespace = "http://www.dhl.com/datatypes")]
public enum WeightUnit1
{

    /// <remarks/>
    K,

    /// <remarks/>
    L,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public partial class ExportLineItemLicense
{

    private string licenseNumberField;

    private System.DateTime expiryDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LicenseNumber
    {
        get { return this.licenseNumberField; }
        set { this.licenseNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime ExpiryDate
    {
        get { return this.expiryDateField; }
        set { this.expiryDateField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum PLTStatus
{

    /// <remarks/>
    A,

    /// <remarks/>
    D,

    /// <remarks/>
    S,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class QtdSInAdCur
{

    private string currencyCodeField;

    private CurrencyRoleTypeCode currencyRoleTypeCodeField;

    private decimal packageChargeField;

    private decimal shippingChargeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string CurrencyCode
    {
        get { return this.currencyCodeField; }
        set { this.currencyCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CurrencyRoleTypeCode CurrencyRoleTypeCode
    {
        get { return this.currencyRoleTypeCodeField; }
        set { this.currencyRoleTypeCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal PackageCharge
    {
        get { return this.packageChargeField; }
        set { this.packageChargeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public decimal ShippingCharge
    {
        get { return this.shippingChargeField; }
        set { this.shippingChargeField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum CurrencyRoleTypeCode
{

    /// <remarks/>
    BILLCU,

    /// <remarks/>
    PULCL,

    /// <remarks/>
    INVCU,

    /// <remarks/>
    BASEC,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class LabelImage
{

    private OutputFormat outputFormatField;

    private bool outputFormatFieldSpecified;

    private byte[] outputImageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OutputFormat OutputFormat
    {
        get { return this.outputFormatField; }
        set { this.outputFormatField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool OutputFormatSpecified
    {
        get { return this.outputFormatFieldSpecified; }
        set { this.outputFormatFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "base64Binary")]
    public byte[] OutputImage
    {
        get { return this.outputImageField; }
        set { this.outputImageField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum OutputFormat
{

    /// <remarks/>
    EPL2,

    /// <remarks/>
    PDF,

    /// <remarks/>
    ZPL2,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class ShipmentTrackingErrorResponse
{

    private ErrorResponse1 responseField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ErrorResponse1 Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class KnownTrackingRequest
{

    private Request1 requestField;

    private string languageCodeField;

    private string[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    private LevelOfDetails levelOfDetailsField;

    private KnownTrackingRequestTrackBy trackByField;

    private bool trackByFieldSpecified;

    private KnownTrackingRequestPiecesEnabled piecesEnabledField;

    private bool piecesEnabledFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request1 Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LanguageCode
    {
        get { return this.languageCodeField; }
        set { this.languageCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AWBNumber", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("LPNumber", typeof(string),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public string[] Items
    {
        get { return this.itemsField; }
        set { this.itemsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName
    {
        get { return this.itemsElementNameField; }
        set { this.itemsElementNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public LevelOfDetails LevelOfDetails
    {
        get { return this.levelOfDetailsField; }
        set { this.levelOfDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public KnownTrackingRequestTrackBy TrackBy
    {
        get { return this.trackByField; }
        set { this.trackByField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TrackBySpecified
    {
        get { return this.trackByFieldSpecified; }
        set { this.trackByFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public KnownTrackingRequestPiecesEnabled PiecesEnabled
    {
        get { return this.piecesEnabledField; }
        set { this.piecesEnabledField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PiecesEnabledSpecified
    {
        get { return this.piecesEnabledFieldSpecified; }
        set { this.piecesEnabledFieldSpecified = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com", IncludeInSchema = false)]
public enum ItemsChoiceType
{

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":AWBNumber")]
    AWBNumber,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":LPNumber")]
    LPNumber,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public enum LevelOfDetails
{

    /// <remarks/>
    LAST_CHECK_POINT_ONLY,

    /// <remarks/>
    ALL_CHECK_POINTS,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
public enum KnownTrackingRequestTrackBy
{

    /// <remarks/>
    GQS,

    /// <remarks/>
    FOCUS,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
public enum KnownTrackingRequestPiecesEnabled
{

    /// <remarks/>
    S,

    /// <remarks/>
    B,

    /// <remarks/>
    P,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class UnknownTrackingRequest
{

    private Request1 requestField;

    private string languageCodeField;

    private string accountNumberField;

    private Reference[] shipperReferenceField;

    private ShipmentDate shipmentDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Request1 Request
    {
        get { return this.requestField; }
        set { this.requestField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LanguageCode
    {
        get { return this.languageCodeField; }
        set { this.languageCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string AccountNumber
    {
        get { return this.accountNumberField; }
        set { this.accountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ShipperReference",
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Reference[] ShipperReference
    {
        get { return this.shipperReferenceField; }
        set { this.shipperReferenceField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentDate ShipmentDate
    {
        get { return this.shipmentDateField; }
        set { this.shipmentDateField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ShipmentDate
{

    private System.DateTime shipmentDateFromField;

    private System.DateTime shipmentDateToField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime ShipmentDateFrom
    {
        get { return this.shipmentDateFromField; }
        set { this.shipmentDateFromField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime ShipmentDateTo
    {
        get { return this.shipmentDateToField; }
        set { this.shipmentDateToField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.dhl.com", IsNullable = false)]
public partial class TrackingResponse
{

    private Response responseField;

    private AWBInfo[] aWBInfoField;

    private PieceFault[] faultField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Response Response
    {
        get { return this.responseField; }
        set { this.responseField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("AWBInfo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public AWBInfo[] AWBInfo
    {
        get { return this.aWBInfoField; }
        set { this.aWBInfoField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public PieceFault[] Fault
    {
        get { return this.faultField; }
        set { this.faultField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class AWBInfo
{

    private string aWBNumberField;

    private Status statusField;

    private ShipmentInfo shipmentInfoField;

    private PieceInfo[] piecesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string AWBNumber
    {
        get { return this.aWBNumberField; }
        set { this.aWBNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Status Status
    {
        get { return this.statusField; }
        set { this.statusField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentInfo ShipmentInfo
    {
        get { return this.shipmentInfoField; }
        set { this.shipmentInfoField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        IsNullable = false)]
    public PieceInfo[] Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ShipmentInfo
{

    private OriginServiceArea originServiceAreaField;

    private DestinationServiceArea destinationServiceAreaField;

    private string shipperNameField;

    private string shipperAccountNumberField;

    private string consigneeNameField;

    private System.DateTime shipmentDateField;

    private object piecesField;

    private string weightField;

    private ShipmentInfoWeightUnit weightUnitField;

    private bool weightUnitFieldSpecified;

    private System.DateTime estDlvyDateField;

    private bool estDlvyDateFieldSpecified;

    private string globalProductCodeField;

    private string shipmentDescField;

    private ShipmentInfoDlvyNotificationFlag dlvyNotificationFlagField;

    private bool dlvyNotificationFlagFieldSpecified;

    private Shipper shipperField;

    private Consignee consigneeField;

    private object[] itemsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public OriginServiceArea OriginServiceArea
    {
        get { return this.originServiceAreaField; }
        set { this.originServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public DestinationServiceArea DestinationServiceArea
    {
        get { return this.destinationServiceAreaField; }
        set { this.destinationServiceAreaField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipperName
    {
        get { return this.shipperNameField; }
        set { this.shipperNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "positiveInteger")]
    public string ShipperAccountNumber
    {
        get { return this.shipperAccountNumberField; }
        set { this.shipperAccountNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConsigneeName
    {
        get { return this.consigneeNameField; }
        set { this.consigneeNameField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.DateTime ShipmentDate
    {
        get { return this.shipmentDateField; }
        set { this.shipmentDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object Pieces
    {
        get { return this.piecesField; }
        set { this.piecesField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentInfoWeightUnit WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool WeightUnitSpecified
    {
        get { return this.weightUnitFieldSpecified; }
        set { this.weightUnitFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public System.DateTime EstDlvyDate
    {
        get { return this.estDlvyDateField; }
        set { this.estDlvyDateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool EstDlvyDateSpecified
    {
        get { return this.estDlvyDateFieldSpecified; }
        set { this.estDlvyDateFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string GlobalProductCode
    {
        get { return this.globalProductCodeField; }
        set { this.globalProductCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ShipmentDesc
    {
        get { return this.shipmentDescField; }
        set { this.shipmentDescField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ShipmentInfoDlvyNotificationFlag DlvyNotificationFlag
    {
        get { return this.dlvyNotificationFlagField; }
        set { this.dlvyNotificationFlagField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DlvyNotificationFlagSpecified
    {
        get { return this.dlvyNotificationFlagFieldSpecified; }
        set { this.dlvyNotificationFlagFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Shipper Shipper
    {
        get { return this.shipperField; }
        set { this.shipperField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public Consignee Consignee
    {
        get { return this.consigneeField; }
        set { this.consigneeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ShipmentEvent", typeof(ShipmentEvent),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("ShipperReference", typeof(Reference),
        Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object[] Items
    {
        get { return this.itemsField; }
        set { this.itemsField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public enum ShipmentInfoWeightUnit
{

    /// <remarks/>
    L,

    /// <remarks/>
    K,

    /// <remarks/>
    G,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.dhl.com/datatypes")]
public enum ShipmentInfoDlvyNotificationFlag
{

    /// <remarks/>
    Y,

    /// <remarks/>
    N,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ShipmentEvent
{

    private System.DateTime dateField;

    private System.DateTime timeField;

    private ServiceEvent serviceEventField;

    private string signatoryField;

    private ServiceArea serviceAreaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime Date
    {
        get { return this.dateField; }
        set { this.dateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "time")]
    public System.DateTime Time
    {
        get { return this.timeField; }
        set { this.timeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceEvent ServiceEvent
    {
        get { return this.serviceEventField; }
        set { this.serviceEventField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Signatory
    {
        get { return this.signatoryField; }
        set { this.signatoryField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceArea ServiceArea
    {
        get { return this.serviceAreaField; }
        set { this.serviceAreaField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class ServiceEvent
{

    private string eventCodeField;

    private string descriptionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string EventCode
    {
        get { return this.eventCodeField; }
        set { this.eventCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class PieceInfo
{

    private PieceDetails pieceDetailsField;

    private PieceEvent[] pieceEventField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PieceDetails PieceDetails
    {
        get { return this.pieceDetailsField; }
        set { this.pieceDetailsField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("PieceEvent", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PieceEvent[] PieceEvent
    {
        get { return this.pieceEventField; }
        set { this.pieceEventField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class PieceDetails
{

    private string aWBNumberField;

    private string licensePlateField;

    private string pieceNumberField;

    private string actualDepthField;

    private string actualWidthField;

    private string actualHeightField;

    private string actualWeightField;

    private string depthField;

    private string widthField;

    private string heightField;

    private string weightField;

    private PackageType packageTypeField;

    private bool packageTypeFieldSpecified;

    private string dimWeightField;

    private string weightUnitField;

    private string pieceContentsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string AWBNumber
    {
        get { return this.aWBNumberField; }
        set { this.aWBNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string LicensePlate
    {
        get { return this.licensePlateField; }
        set { this.licensePlateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceNumber
    {
        get { return this.pieceNumberField; }
        set { this.pieceNumberField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActualDepth
    {
        get { return this.actualDepthField; }
        set { this.actualDepthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActualWidth
    {
        get { return this.actualWidthField; }
        set { this.actualWidthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActualHeight
    {
        get { return this.actualHeightField; }
        set { this.actualHeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ActualWeight
    {
        get { return this.actualWeightField; }
        set { this.actualWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Depth
    {
        get { return this.depthField; }
        set { this.depthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Width
    {
        get { return this.widthField; }
        set { this.widthField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Height
    {
        get { return this.heightField; }
        set { this.heightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Weight
    {
        get { return this.weightField; }
        set { this.weightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public PackageType PackageType
    {
        get { return this.packageTypeField; }
        set { this.packageTypeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PackageTypeSpecified
    {
        get { return this.packageTypeFieldSpecified; }
        set { this.packageTypeFieldSpecified = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DimWeight
    {
        get { return this.dimWeightField; }
        set { this.dimWeightField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string WeightUnit
    {
        get { return this.weightUnitField; }
        set { this.weightUnitField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceContents
    {
        get { return this.pieceContentsField; }
        set { this.pieceContentsField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class PieceEvent
{

    private System.DateTime dateField;

    private System.DateTime timeField;

    private ServiceEvent serviceEventField;

    private string signatoryField;

    private ServiceArea serviceAreaField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "date")]
    public System.DateTime Date
    {
        get { return this.dateField; }
        set { this.dateField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified,
        DataType = "time")]
    public System.DateTime Time
    {
        get { return this.timeField; }
        set { this.timeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceEvent ServiceEvent
    {
        get { return this.serviceEventField; }
        set { this.serviceEventField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Signatory
    {
        get { return this.signatoryField; }
        set { this.signatoryField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public ServiceArea ServiceArea
    {
        get { return this.serviceAreaField; }
        set { this.serviceAreaField = value; }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.dhl.com/datatypes")]
public partial class PieceFault
{

    private string pieceIDField;

    private string conditionCodeField;

    private string conditionDataField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string PieceID
    {
        get { return this.pieceIDField; }
        set { this.pieceIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionCode
    {
        get { return this.conditionCodeField; }
        set { this.conditionCodeField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string ConditionData
    {
        get { return this.conditionDataField; }
        set { this.conditionDataField = value; }
    }
}