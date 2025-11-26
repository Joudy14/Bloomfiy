// Event booking functionality
document.addEventListener('DOMContentLoaded', function () {
    const bookingForm = document.getElementById('eventBookingForm');
    const successMessage = document.getElementById('bookingSuccess');

    if (bookingForm) {
        bookingForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Simple form validation
            if (validateForm()) {
                // Hide form and show success message
                bookingForm.style.display = 'none';
                successMessage.style.display = 'block';

                // Scroll to success message
                successMessage.scrollIntoView({ behavior: 'smooth' });
            }
        });
    }
});

function validateForm() {
    const requiredFields = document.querySelectorAll('#eventBookingForm [required]');
    let isValid = true;

    requiredFields.forEach(field => {
        if (!field.value.trim()) {
            isValid = false;
            field.style.borderColor = 'red';
        } else {
            field.style.borderColor = '#e0e0e0';
        }
    });

    if (!isValid) {
        alert('Please fill in all required fields.');
    }

    return isValid;
}