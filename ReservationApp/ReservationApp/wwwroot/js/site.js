document.addEventListener('DOMContentLoaded', function () {
    // Check if the user has already set the night mode preference
    const isNightMode = localStorage.getItem('nightMode') === 'true';

    // Set the appropriate class for the body based on the preference
    document.body.classList.toggle('night-mode', isNightMode);

    // Set the text of the button according to the current mode
    const toggleModeBtn = document.getElementById('toggleModeBtn');
    toggleModeBtn.innerText = isNightMode ? 'Switch to Day Mode' : 'Switch to Night Mode';

    // Handle button click
    toggleModeBtn.addEventListener('click', function () {
        // Change night mode preference
        const newMode = !isNightMode;
        localStorage.setItem('nightMode', newMode);

        // Apply changes to the body class
        document.body.classList.toggle('night-mode', newMode);

        // Change the text of the button
        toggleModeBtn.innerText = newMode ? 'Switch to Day Mode' : 'Switch to Night Mode';

        location.reload()
    });
});