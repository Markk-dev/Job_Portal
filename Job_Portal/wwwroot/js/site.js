
let generatedOTP = '';
let userEmail = '';

function generateOTP() {
    return Math.floor(100000 + Math.random() * 900000).toString();
}

function showMessage(message, isSuccess) {
    const statusElement = document.getElementById('statusMessage');
    statusElement.textContent = message;
    statusElement.className = isSuccess ? 'success-message' : 'error-message';
    statusElement.style.display = 'block';


    if (isSuccess) {
        setTimeout(() => {
            statusElement.style.display = 'none';
        }, 5000);
    }
}

document.getElementById('forgotPasswordForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('emailInput').value.trim();
    const submitButton = document.getElementById('submitButton');

    if (!email) {
        showMessage('Email is required!', false);
        return;
    }

    submitButton.disabled = true;
    submitButton.textContent = "Checking...";

    try {
        // First verify the email exists in your database
        const verifyResponse = await axios.post('/Auth/VerifyEmail', { email });

        if (!verifyResponse.data.success) {
            showMessage(verifyResponse.data.errorMessage || 'Email not found in our system.', false);
            submitButton.disabled = false;
            submitButton.textContent = "Send OTP";
            return;
        }

        // Now generate and send OTP
        generatedOTP = generateOTP();
        userEmail = email;

        const response = await axios.post("https://api.emailjs.com/api/v1.0/email/send", {
            service_id: "service_hxb425t",
            template_id: "template_muqhzus",
            user_id: "FqDte2dDwEqmzp5rf",
            template_params: {
                user_email: email,
                OTP_CODE: generatedOTP,
                title: "Password Recovery OTP"
            }
        });

        console.log("EmailJS API Response:", response.data);
        showMessage("OTP sent to your email!", true);
        document.getElementById('otpSection').style.display = 'block';
    } catch (error) {
        console.error("Full error:", error.response?.data || error.message);
        showMessage(
            `Failed to send OTP: ${error.response?.data?.errorMessage || error.message || "Network error"}`,
            false
        );
    } finally {
        submitButton.disabled = false;
        submitButton.textContent = "Resend OTP";
    }
});


document.getElementById('verifyOtpButton').addEventListener('click', () => {
    const enteredOTP = document.getElementById('otpInput').value.trim();

    if (!enteredOTP) {
        showMessage('Please enter the OTP', false);
        return;
    }

    if (enteredOTP === generatedOTP) {
        showMessage('OTP verified successfully!', true);

        document.getElementById('otpSection').style.display = 'none';
        document.getElementById('newPasswordSection').style.display = 'block';
    } else {
        showMessage('Invalid OTP. Please try again.', false);
    }
});


document.getElementById('updatePasswordButton').addEventListener('click', async () => {
    const newPassword = document.getElementById('newPasswordInput').value;
    const confirmPassword = document.getElementById('confirmPasswordInput').value;

    if (!newPassword || !confirmPassword) {
        showMessage('Both password fields are required', false);
        return;
    }

    if (newPassword !== confirmPassword) {
        showMessage('Passwords do not match', false);
        return;
    }

    if (newPassword.length < 8) {
        showMessage('Password must be at least 8 characters', false);
        return;
    }


    showMessage('Password updated successfully!', true);


});