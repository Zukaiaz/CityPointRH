// accessibility.js

let body;

document.addEventListener("DOMContentLoaded", function () {
    body = document.body;

    // Load saved preferences
    if (localStorage.getItem("theme") === "dark") {
        body.classList.add("dark-mode");
    }

    if (localStorage.getItem("contrast") === "high") {
        body.classList.add("high-contrast");
        // Remove dark mode if high contrast is active
        body.classList.remove("dark-mode");
        localStorage.removeItem("theme");
    }

    const savedFontSize = localStorage.getItem("fontSize");
    if (savedFontSize) {
        body.style.fontSize = savedFontSize;
    }

    // Toggle accessibility menu
    const accessibilityButton = document.getElementById("accessibility-toggle");
    const accessibilityMenu = document.getElementById("accessibility-menu");

    if (accessibilityButton) {
        accessibilityButton.addEventListener("click", function () {
            accessibilityMenu.classList.toggle("active");
        });
    }
});

function toggleDarkMode() {
    if (!body) body = document.body;

    // If high contrast is on, turn it off first
    if (body.classList.contains("high-contrast")) {
        body.classList.remove("high-contrast");
        localStorage.removeItem("contrast");
    }

    body.classList.toggle("dark-mode");
    localStorage.setItem("theme", body.classList.contains("dark-mode") ? "dark" : "light");
}

function toggleHighContrast() {
    if (!body) body = document.body;

    // If dark mode is on, turn it off first
    if (body.classList.contains("dark-mode")) {
        body.classList.remove("dark-mode");
        localStorage.removeItem("theme");
    }

    body.classList.toggle("high-contrast");
    localStorage.setItem("contrast", body.classList.contains("high-contrast") ? "high" : "normal");
}

function changeFontSize(action) {
    if (!body) body = document.body;
    let currentSize = parseInt(window.getComputedStyle(body).fontSize);

    if (action === "increase") {
        currentSize += 2;
    } else if (action === "decrease") {
        currentSize -= 2;
    }

    body.style.fontSize = currentSize + "px";
    localStorage.setItem("fontSize", currentSize + "px");
}

function speakText() {
    let text = document.body.innerText;
    let speech = new SpeechSynthesisUtterance(text);
    speech.lang = "en-GB";
    window.speechSynthesis.speak(speech);
}

// Make functions globally accessible
window.toggleDarkMode = toggleDarkMode;
window.toggleHighContrast = toggleHighContrast;
window.changeFontSize = changeFontSize;
window.speakText = speakText;