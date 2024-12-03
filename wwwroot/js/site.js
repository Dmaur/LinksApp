function startAutoLogout() {
    // Set auto logout after 20 minutes (20 minutes = 20 * 60000 milliseconds)
    window.setTimeout(() => document.location = "/", 20 * 60000);
    
    // Warn user 2 minutes (18 minutes = 18 * 60 * 1000 milliseconds) before session expiry
    window.setTimeout(() => {
        document.getElementById("lblExpire").innerHTML = "WARNING: Session is about to expire!";
    }, 18 * 60000);
}
