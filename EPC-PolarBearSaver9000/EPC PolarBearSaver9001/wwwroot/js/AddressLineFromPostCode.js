$(document).ready(function () {
    var postcodeTextBox = document.getElementById("PostcodeTextBox");
    var addressDropdown = document.getElementById("AddressLineDropdown");
    postcodeTextBox.addEventListener("change", function (evt) {
        GetValidAddressLines();
    });
    addressDropdown.addEventListener("change", function (evt) {
        EnableEPCScoreButton();
    });
});

function EnableEPCScoreButton() {
    var generateScoreButton = document.getElementById("GenerateScoreButton");
    if (generateScoreButton !== undefined && generateScoreButton !== null) {
        generateScoreButton.disabled = false;
    }
}

function UpdateAddressDropdown(addressLineOptions) {
    var addressDropdown = document.getElementById("AddressLineDropdown");
    addressDropdown.innerHTML = "";    
    if (addressLineOptions.length > 0) {
        addressDropdown.innerHTML = addressLineOptions;
        addressDropdown.disabled = false;
    } else {
        addressDropdown.disabled = true;
    }

}

function GenerateAddressLineOptions(response) {
    var optionsInnerHTML = "";
    for (var row in response) {
        var address = response[row];
        optionsInnerHTML += "<option value=\"" + address + "\">" + address +"</option>";
    }
    return optionsInnerHTML;
}

function GetValidAddressLines() {
    var postcode = document.getElementById("PostcodeTextBox").value.replace(" ", "");
    var searchTerm = URL + ADDRESS_END_POINT + postcode;
    var request = new XMLHttpRequest();
    request.open('GET', searchTerm);
    request.responseType = 'json';
    request.send();

    request.onload = function () {
        var optionsInnerHTML = GenerateAddressLineOptions(request.response);
        UpdateAddressDropdown(optionsInnerHTML);
    };
}

