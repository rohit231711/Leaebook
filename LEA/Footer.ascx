<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Includes_Footer" %>
<div class="footer">
    <div class="edit-shadow">
        <!--<div class="shadow">
            <img src="images/edit-shadow.png" alt="" /></div>!-->
    </div>
    <div class="wrap">
        <div class="footermain">
            <div class="aboutusmenu">
                <h2>
                    <Localized:LocalizedLiteral ID="lblaboutus" runat="server" Key="aboutus" Colon="false" />
                </h2>
                <div class="ftpart1">
                    <ul>
                        <li><a id="aboutus" runat="server" href="CMS_Content.aspx?id=2&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblaboutus1" runat="server" Key="aboutus" Colon="false" />
                        </a></li>
                        <li><a id="contactus" runat="server" href="ContactUs.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblcontactus" runat="server" Key="contactus" Colon="false" />
                        </a></li>
                        <!--  <li><a href="CMS_Content.aspx?id=11&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblcareers" runat="server" Key="careers" Colon="false" />
                        </a></li>
                        <li><a href="CMS_Content.aspx?id=12&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblaffiliate" runat="server" Key="affiliate" Colon="false" />
                        </a></li>
                        <li><a href="CMS_Content.aspx?id=13&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblprogram" runat="server" Key="program" Colon="false" />
                        </a></li>
                        <li><a href="CMS_Content.aspx?id=10&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblprivacypolicy" runat="server" Key="privacypolicy"
                                Colon="false" />
                        </a></li>    -->
                        <li><a id="sitemap" runat="server" href="CMS_Content.aspx?id=14&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblsitemap" runat="server" Key="sitemap" Colon="false" />
                        </a></li>
                        <!--  <li class="border">
                            <a href="CMS_Content.aspx?id=20&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblgiftcards" runat="server" Key="giftcards" Colon="false" />                         
                            </a>
                        </li>  -->
                    </ul>
                </div>
            </div>
            <!--    <div class="aboutusmenu1">
                <h2>
                    <Localized:LocalizedLiteral ID="lblresources" runat="server" Key="resources" Colon="false" />
                </h2>
                <div class="ftpart1sec">
                    <ul>
                        <li>
                            <a href="CMS_Content.aspx?id=15&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblshopbybrand" runat="server" Key="shopbybrand" Colon="false" />
                            </a>
                        </li>
                        <li>
                            <a href="CMS_Content.aspx?id=16&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblguaranteedreturns" runat="server" Key="guaranteedreturns" Colon="false" />
                            </a>
                        </li>
                        <li>
                            <a href="CMS_Content.aspx?id=17&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblshipping" runat="server" Key="shipping" Colon="false" />
                            </a>
                        </li>
                        <li>
                            <a href="CMS_Content.aspx?id=18&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblfitcalculator" runat="server" Key="fitcalculator" Colon="false" />
                            </a>
                        </li>
                        <li>
                            <a href="CMS_Content.aspx?id=19&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblhelpcenter" runat="server" Key="helpcenter" Colon="false" />                        
                            </a>
                        </li>
                       
                    </ul>
                </div>
            </div> -->
            <div class="aboutusmenu3">
                <h2>
                    <Localized:LocalizedLiteral ID="lblinformation" runat="server" Key="information" Colon="false" />
                </h2>
                <div class="ftpart1sec1">
                    <ul>
                        <!-- <li>
                            <a href="CMS_Content.aspx?id=2&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblaboutus2" runat="server" Key="aboutus" Colon="false" />
                            </a>
                        </li> -->
                        <li>
                            <a id="deliveryinfo" runat="server" href="CMS_Content.aspx?id=21&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lbldeliveryinformation" runat="server" Key="deliveryinformation" Colon="false" />
                            </a>
                        </li>
                        <li><a id="privacy" runat="server" href="CMS_Content.aspx?id=10&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblprivacypolicy1" runat="server" Key="privacypolicy" Colon="false" />
                        </a></li>
                        <li><a id="terms" runat="server" href="CMS_Content.aspx?id=5&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblterms" runat="server" Key="terms" Colon="false" />
                        </a></li>
                        <li>
                            <a id="refundPolicy" runat="server" href="CMS_Content.aspx?id=27&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblrefundPolicy" runat="server" Key="refundPolicy" Colon="false" />
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="aboutusmenu4">
                <h2>
                    <Localized:LocalizedLiteral ID="lblemailsignup" runat="server" Key="emailsignup" Colon="false" />
                </h2>
                <div class="ftpart1sec4">
                    <ul>
                        <li><a id="latestnews" runat="server" href="CMS_Content.aspx?id=22&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lbllatestnews" runat="server" Key="latestnews" Colon="false" />
                            &amp; 
                             <Localized:LocalizedLiteral ID="lblpromotions" runat="server" Key="promotions" Colon="false" />
                        </a></li>
                        <li><a id="unsubscribe" runat="server" href="CMS_Content.aspx?id=23&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblunsubscribe" runat="server" Key="unsubscribe" Colon="false" />
                        </a></li>
                        <li>
                            <a id="partner" runat="server" href="PartnerRegister.aspx">
                                <Localized:LocalizedLiteral ID="lblPartner" runat="server" Key="RegisterPartner" Colon="false"></Localized:LocalizedLiteral>
                            </a>
                        </li>
                        <li class="border"><img src="../images/paypal.png" alt="" style="margin-top: 10px;" /></li>
                    </ul>
                </div>
            </div>

            <div class="aboutusmenu4">
                <div class="ftpart1sec4">
                    <div class="googlebu">
                        <a href="https://play.google.com/store/apps/details?id=com.vrin.leaebooks&hl=en" target="_blank">
                            <img src="../images/googleplaystore.png" alt="" /></a>
                        <a href="https://itunes.apple.com/us/app/lea-ebooks/id960249388?ls=1&mt=8" target="_blank">
                            <img src="../images/appstoreimg.png" alt="" /></a>
                        <a href="https://www.leaebook.com/ReaderSetup/LEAeBooks.exe">
                            <img src="../images/download-windows.png" alt="" /></a>
                        <div class="inform_popup">
                            <a class="tooltip" href="https://www.leaebook.com/ReaderSetup/LEAeBooks.dmg" title="How to install reader in Mac? Please follow below steps,
1. Click on “Download reader for Mac
2. After downloaded go in your Mac System and Preferences.
3. Click on General &gt; click the lock to make changes &gt; select anywhere
4. Now click the lock to prevent further changes
5. Now close System and Preferences.
6. Find destination where reader was download then click on it for complete set up.">
                                <img src="../images/download-mac.png" alt="" />
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="aboutusmenu5 foot-sco-icon">
                <h2>
                    <Localized:LocalizedLiteral ID="lblsocialmedia" runat="server" Key="socialmedia" Colon="false" />
                </h2>
                <a style="margin-right: 5px;" id="fb" runat="server" target="_blank">
                    <img src="../images/fbicon1.png" alt="" /></a>
                <a style="margin-right: 5px;" id="ti" runat="server" target="_blank">
                    <img src="../images/twitericon2.png" alt="" /></a>
                <a id="gp" runat="server" target="_blank">
                    <img src="../images/gplusicon2.png" alt="" /></a>
                  <a id="A1" runat="server" target="_blank">
                    <img src="../images/instagram-logo.png" alt="" /></a>

                <div class="paypal">
                    <span id="siteseal" class="godaddy1">
                        <%--<script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=mow0OrbLF1oR3bU1WgzVUJaXqfYqMG0o37NalEmINxNxHZaYtcTAeGrhYZiu"></script>--%>
                    </span>
                    <img src="../images/PayPalCredit_1.png" alt="" />
                    <img src="../images/PayPalCredit_2.png" alt="" />
                </div>
            </div>
        </div>
    </div>
    <div class="copyrightmsg">
        &copy; Copyright <%= DateTime.Now.Year.ToString() %>. LEA eBooks,
        <Localized:LocalizedLiteral ID="lblrightsreserved" runat="server" Key="rightsreserved" Colon="false" />
    </div>
</div>

