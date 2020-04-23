<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReleaseOfLiability.aspx.vb" Inherits="Phazzer.ReleaseOfLiability" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>PT Release of Liability</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" /> 

       <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
           .auto-style2 {
               height: 48px;
           }
           .auto-style3 {
               width: 301px;
           }
    </style>

</head>
<body>
    <form id="form1" runat="server">
               <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="txtEmail">
                    <UpdatedControls>

                        <telerik:AjaxUpdatedControl ControlID="lblname" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="btnSubmit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSubmit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblerr" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="lblRdTimerText" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="timerRD">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lblRdTimer" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
    </div>
    <div class="col text-right text-sm-right" style="padding-right:50px;" ><br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" /> &nbsp; &nbsp; <asp:LoginName ID="LoginName1" runat="server" />
    </div>
</div><%-- end top banner--%>
        			<telerik:RadMenu RenderMode="Auto" ID="RadMenu1" runat="server" EnableRoundedCorners="true" ShowToggleHandle="true"
            EnableShadows="true" EnableTextHTMLEncoding="true" DataValueField="Text" Style="position:absolute;z-index:3000" 
                DataSourceID="XmlDataSource1" DataNavigateUrlField="Url" DataTextField="Text" 
                Width="100%" />
            
            <asp:XmlDataSource ID="XmlDataSource0" runat="server" DataFile="~/menus/adminMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/menus/clerkMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/menus/Manager.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource3" runat="server" DataFile="~/menus/InstructorMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource4" runat="server" DataFile="~/menus/StudentMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource5" runat="server" DataFile="~/menus/ClientMenu.xml" XPath="/Items/Item" />
<div id="wrapper" style="text-align: center">    
    <div id="yourdiv" class="pt-3"><br />
        <h3>   Student and Volunteer Warnings<br />
Liability Release and Agreement Not to Sue</h3>
</div>
</div>
<table style="align-self:center;width:75%; font-size:14px; margin:auto; "><tr><td>
        <div class="auto-style1">
            Any person participating as a student or volunteers to experience a PhaZZer device electrical discharge (“PhaZZer Exposure”)<br />
            &nbsp;must read and sign this form prior to any Exposure.
            <br />
            <b>Warnings</b>        

        </div>
<table style="border:1px solid black;"><tr><td>
<ol>PhaZZer™ devices are designed to significantly reduce the likelihood of serious injuries or death. When used as directed, they have been found to be a safer and more effective alternative to other traditional use of force tools and techniques.  Any use of force, however, carries with it risks of injury or even death.  To minimize these risks, read and understand all warnings before experiencing a PhaZZer Exposure.

<li>	Like a Firearm, always assume that a PhaZZer device is loaded.</li>
<li>	Keep your hands and other body parts away from the front of the PhaZZer cartridge.</li>
<li>	The PhaZZer device can cause temporary discomfort, pain, stress, and panic which may be injurious to some people.</li>
<li>	The PhaZZer device will cause strong muscle contractions that may cause physical exertion or athletic type injuries to some people. These muscle contractions can result in strain-type injuries such as hernias, ruptures, or other injuries to soft tissue, organs, muscles, tendons, ligaments, nerves, joints, and stress/compression fractures to bones, including vertebrae. People with preexisting injuries or conditions such as osteoporosis, osteopenia, spinal injuries, diverticulitis, or previous muscle, disc, ligament, or tendon damage may be more susceptible to these types of injuries.</li>
<li>	Due to these strong muscle contractions can render a subject temporarily unable to control his or her movements secondary injuries may occur. Under certain circumstances, this loss of control can elevate the risk(s) of serious injury or death. These circumstances may include, but are not limited to, use of the PhaZZer device on a person who is physically infirm or pregnant, or a person on an elevated or unstable platform, operating a vehicle or machinery, running, or in water where the inability to move may result in drowning. These will be reviewed more in depth during class.</li>
<li>	The stress and exertion of extensive repeated, prolonged, or continuous application(s) of the PhaZZer device may contribute to cumulative exhaustion, stress, and associated medical risk(s).  Severe exhaustion and/or over-exertion from physical struggle, drug intoxication, use of restraint devices, etc., may result in serious injury or death. The PhaZZer device causes strong muscle contractions, usually rendering a subject temporarily unable to control his or her movements.  Under certain circumstances, these contractions may impair a subject’s ability to breathe.  If a person’s system is already
compromised by over-exertion, drug intoxication, stress, pre-existing medical or psychological condition(s), etc., any physical exertion, including the use of a PhaZZer device, may have an additive effect in contributing to cumulative exhaustion, stress, cardiovascular conditions, and associated medical risk(s).</li>
<li>	Avoid touching the probes and wires and the areas between the probes during PhaZZer
electrical discharge.</li>
<li>	Avoid intentionally aiming a PhaZZer device at the head, face or other sensitive areas. The
preferred target area is the subject’s torso (center mass, below the heart), legs or back.</li>
<ol type="a"><li>PhaZZer probes can cause significant injury if deployed into sensitive areas of the body such as the eyes, throat, or genitals.</li>
<li>	If a PhaZZer probe becomes embedded in an eye, it could result in permanent loss of vision.</li>
<li>	Repetitive stimuli such as flashing lights or electrical stimuli can induce seizures in some individuals. This risk is heightened if electrical stimuli or current passes through the head region.</li>
</ol><li>	In most areas of the body, wounds caused by PhaZZer probes will be minor.  PhaZZer probes have small barbs at the end. This is will be covered in detail during the training session.</li>
<li>	Use of a PhaZZer device in stun mode can cause burn marks, friction abrasions, and/or scarring
that may be permanent depending on individual susceptibilities or circumstances surrounding PhaZZer
device use and exposure.</li>
<li>	PhaZZer devices can ignite gasoline, other flammables, or explosive vapors (e.g., gases found in sewer lines). Some self-defense sprays use flammable carriers such as alcohol and could be dangerous to use in immediate conjunction with PhaZZer devices.</li>
<li>	The PhaZZer device is a sophisticated electronic system. Only PhaZZer, Inc. approved components and proper accessories may be used with the PhaZZer device in order to ensure proper function and effects. Use of anything other than recommended batteries, PhaZZer Cartridges, or other PhaZZer-recommended accessories (excluding holsters) may cause malfunctions, will void the warranty, and may put the user and/or others at risk of serious injury or even death.</li>
<li>	Dropping a PhaZZer device may damage the unit. If a weapon has been dropped or damaged,
or if a weapon has been exposed to significant moisture, do not attempt to place the safety switch in the
UP (FIRE) position until you have contacted PhaZZer, Inc. customer service.</li>
<li>	The PhaZZer device incorporates a laser aiming aid. Laser beams can cause eye damage.  Avoid direct eye exposure.</li>
<li>Any person volunteering to  experience a PhaZZer Exposure shall be supported by two spotters to prevent the volunteer from falling. Each spotter should hold an upper arm under the armpit, so the volunteer may be safely supported and lowered to the ground. Eye protection is required for both the volunteer and two spotters.  </li>
<li>	Because of parental/guardian consent issues, no minor shall be voluntarily exposed to a
PhaZZer device.</li>
</ol> 
</td></tr></table><br />
            <div class="auto-style1">
<b>LIABILITY RELEASE, COVENANT NOT TO SUE AND HOLD HARMLESS</b>
<br />
        </div>
<table style="border:1px solid black;"><tr><td><ol>In consideration of receiving a PhaZZer Exposure, I acknowledge and agree as follows:
<li>I understand that a PhaZZer Exposure results in strong muscle contractions, physical exertion and stress and involves the risk of physical injury.  I acknowledge that I have read the above Warnings and Risks and with full knowledge of such risks, I voluntary agree to experience a PhaZZer Exposure and I assume all risks, whether known or unknown, foreseen or unforeseen, inherent in the PhaZZer Exposure.</li>
<li>Intending that this Form be legally binding upon me, my heirs, executors, administrators, and assigns, I hereby waive, release, and forever discharge the instructor, my law enforcement agency, PhaZZer, Inc., Phazzer Training Group, and all of its agents, directors and employees of
and from any and all claims, demands, rights and causes of action of whatsoever kind and nature, arising
from, and by reason of any and all known and unknown physical and mental injuries and consequences thereof, whether foreseen or unforeseen, suffered by me from any and all activities during the Training class, including the PhaZZer Exposure.</li>
<li>	I further agree that neither I nor my heirs, estate, personal representative, nor any other person
or entity will ever institute any action, litigation or suit at law or in equity against the instructor, my law enforcement agency, PhaZZer Electronics Inc., Phazzer Training Group, Blue Line Training and Consulting, LLC, and all of its agents, directors and employees for any damages, costs, loss or injury arising out of any and all activities during the training class, including the PhaZZer Exposure.</li>
<li>	I further agree to indemnify and save harmless the instructor, my law enforcement agency, PhaZZer Electronics Inc., Phazzer Training Group, Blue Line Training and Consulting, LLC, and all of its agents, directors and employees from all liability, loss, costs and obligation of any and every kind on account of or arising out of any injuries or losses, however occurring, arising out of any and all activities during the training class, including the PhaZZer Exposure.</li>
<li>	In signing this Form, I agree that I have read and understand this entire Form; I understand that it is a promise not to sue and a release and indemnity for all claims; I further understand that by signing this Form I am giving up certain legal rights including the right to recover damages in case of injury; and I agree to abide by the terms and conditions of this Form. I also understand that certain state laws prohibit a release of claims which are not known or suspected to exist at the time a release is signed, and with full knowledge of such laws, I agree to expressly waive any rights I may have under such laws
or common law principles of similar effect</li>
</ol>
</td></tr></table>
    </td></tr>
    <tr><td class="border border-dark">
        <div class="row pl-4">
            <div class="col">
                <div class="row">
            eMail address (from registration form)<br />
            <telerik:RadTextBox ID="txtEmail" Width="265px"  runat="server" AutoPostBack="true" EmptyMessage="eMail Adress (from Registration form)" />
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail" runat="server" 
                   Text="invalid eMail address" ForeColor="red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </div>
                <div class="row">
                    Booking ID &nbsp; &nbsp;
                    <telerik:RadTextBox ID="txtBookingID" runat="server" EmptyMessage="Booking ID" Width="100px" />
                    <asp:RequiredFieldValidator ID="requiredfieldvalidator1" runat="server" ControlToValidate="txtBookingID" ForeColor="Red" Text="required " /></div>
                <div class="row">
                    <asp:Label ID="lblname" Text="" runat="server" />
                </div>
            </div>
            <div class="col pl-5">
                <div class="row">
                    Your IP Address:<br /><asp:Label ID="lblIPaddress" runat="server" />
                </div>
                <div class="row">
                    TimeStamp:<br /><asp:Label ID="lblTimeStamp" Font-Size="Smaller" runat="server" />
                </div>
                <div class="row pt-2">
                    <telerik:RadButton ID="btnSubmit" runat="server" Font-Bold="true" CssClass= "btn-success" BackColor="LightGreen" Text="Click to Agree" />
                </div>
            </div>
            <div class="col-3 pl-5">
                        Phazzer Training Group<br />
                        139 South McDonald Street<br />
                        McDonald, PA. 15057<br />
                PhaZZer Training Group<br />
                Blue Line Training and Consulting, LLC<br />
                PhaZZerTraining.com
            </div>
            <div class="col-4">
                &nbsp;
            </div>
        </div>
                   <asp:Label CssClass="h5" ID="lblerr" runat="server" />
<div><table><tr><td><asp:label id="lblRdTimerText" runat="server" Text="Redirecting to PhazzerTraining.com in: " /></td><td> <asp:Label ID="lblRdTimer" runat="server" /></td></tr></table></div>  </td></tr></table>
        <asp:Timer ID="timerRD" runat="server" /></td></tr></table>
        <br /> <br />&nbsp;
    </form>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
