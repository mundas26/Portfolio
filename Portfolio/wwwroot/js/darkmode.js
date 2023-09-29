// Get elements
const themeSwitch = document.getElementById("toggleBtn");
const body = document.body;

// Function to set the theme mode in localStorage
const setThemeMode = (mode) => {
    localStorage.setItem("theme", mode);
};

// Function to load the theme mode from localStorage
const loadThemeMode = () => {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark-mode") {
        body.classList.add("dark-mode");
        themeSwitch.checked = true;
    }
};

// Load the theme mode when the page loads
window.addEventListener("load", loadThemeMode);

themeSwitch.addEventListener("change", () => {
    body.classList.toggle("dark-mode");
    if (body.classList.contains("dark-mode")) {
        setThemeMode("dark-mode");
    } else {
        setThemeMode("light-mode");
    }
});