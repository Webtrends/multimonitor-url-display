//Copyright 2014 Webtrends Inc.
//Authored by:  Warren Bebout
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using System;

namespace WebBrowserControlDialogs
{
    public static class StringConstants
    {
        internal const String DialogCaptionSecurityAlert = "Security Alert";
        internal const String DialogCaptionWindowsInternetExplorer = "Windows Internet Explorer";
        internal const String DialogCaptionMicrosoftInternetExplorer = "Microsoft Internet Explorer";
        internal const String DialogCaptionScriptError = "Script Error";
        internal const String DialogCaptionConnectTo = "Connect to";

        internal const String DialogTextNonSecureToSecureWarning = "You are about to view pages over a secure connection.";
        internal const String DialogTextSecureToNonSecureWarning = 
            "You are about to leave a secure Internet connection.  It will be possible for others to view information you send.";

        internal const String DialogCaptionDoYouWantToCloseThisWindow = "Do you want to close this window?";
        internal const String GCHandleTargetIsNotExpectedType = "GCHandle target is not expected type";

        internal const String ButtonTextYes = "&Yes";
        internal const String ButtonTextOk = "OK";

        internal const String WindowTypeCombo = "ComboBoxEx32";
        internal const String WindowTypeEdit = "Edit";
        internal const String WindowTypeButton = "Button";
    }
}
