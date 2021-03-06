﻿$(document).ready(function () {
    var generateScoreButton = document.getElementById("GenerateScoreButton");
    generateScoreButton.addEventListener("click", function (evt) {
        GetScore();
        evt.preventDefault();
    });
    CheckAddressPrefilled();
});

//TODO: move request into site.js function to share across files
function GetScore() {
    var postcode = document.getElementById("PostcodeTextBox").value.replace(" ", "");
    var addressDropdown = document.getElementById("AddressLineDropdown");
    if (addressDropdown !== undefined && addressDropdown !== null) {
        var address = addressDropdown.value;
        var searchTerm = URL + SCORE_END_POINT + postcode + "/" + address;
        var request = new XMLHttpRequest();
        request.open('GET', searchTerm);
        request.responseType = 'json';
        request.send();

        request.onload = function () {
            var score = request.response;
            DisplayScore(score);
            DisplaySavings(score);
            UpdateScoreColour(score);
        };
    }

}

function DisplayScore(score) {
    var scoreLabel = document.getElementById("EPCScoreLabel");
    scoreLabel.innerHTML = score;
}

function DisplaySavings(score) {
    var scoreLabel = document.getElementById("SavingsLabel");
    var savings = 400 * ((100 - score) / 100);
    scoreLabel.innerHTML = "You could save £" + savings;
}

function UpdateScoreColour(score) {
    var scoreBackground = document.getElementById("EPCScoreLabel").parentElement.parentElement;
    scoreBackground.style.backgroundColor = CalculateScoreColour(score);
}

function CalculateScoreColour(score){
    var r = CalculateColourWeighting(100 - score);
    var g = CalculateColourWeighting(score);
    return 'rgb(' + r + ',' + g + ',' + 0 + ')';
}

function CalculateColourWeighting(score) {
    if (score > 50) {
        return 255;
    }
    return (score / 50) * 255;
}

function CheckAddressPrefilled() {
    var postCodeTextBox = document.getElementById("PostcodeTextBox");
    var addressLineDropdown = document.getElementById("AddressLineDropdown");
    var generateScoreButton = document.getElementById("GenerateScoreButton");

    if (postCodeTextBox !== null && generateScoreButton !== null) {
        if (postCodeTextBox.value.length > 0 && addressLineDropdown.value.length > 0) {
            generateScoreButton.disabled = false;
            addressLineDropdown.disabled = false;
            GetScore();
        }
    }

}