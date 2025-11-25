// Terms of Service Page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Fade-in animation for terms sections
    const termsSections = document.querySelectorAll('.terms-section');
    
    const sectionObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateX(0)';
            }
        });
    }, { threshold: 0.2 });

    termsSections.forEach((section, index) => {
        section.style.opacity = '0';
        section.style.transform = 'translateX(-30px)';
        section.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        section.style.transitionDelay = `${index * 0.1}s`;
        sectionObserver.observe(section);
    });

    // Add table of contents functionality
    const createTableOfContents = () => {
        const sections = document.querySelectorAll('.terms-section h2');
        const tocContainer = document.createElement('div');
        tocContainer.className = 'table-of-contents';
        
        let tocHTML = '<h3>Table of Contents</h3><ul>';
        
        sections.forEach((section, index) => {
            const id = `section-${index + 1}`;
            section.id = id;
            tocHTML += `<li><a href="#${id}">${section.textContent}</a></li>`;
        });
        
        tocHTML += '</ul>';
        tocContainer.innerHTML = tocHTML;
        
        // Insert after last-updated div
        const lastUpdated = document.querySelector('.last-updated');
        lastUpdated.parentNode.insertBefore(tocContainer, lastUpdated.nextSibling);
        
        // Add TOC styles
        const style = document.createElement('style');
        style.textContent = `
            .table-of-contents {
                background: white;
                padding: 30px;
                border-radius: 10px;
                box-shadow: 0 3px 15px rgba(0,0,0,0.08);
                margin-bottom: 40px;
            }
            .table-of-contents h3 {
                color: #5a7c5a;
                margin-bottom: 15px;
                font-weight: 500;
            }
            .table-of-contents ul {
                list-style: none;
                padding: 0;
            }
            .table-of-contents li {
                margin-bottom: 8px;
            }
            .table-of-contents a {
                color: #666;
                text-decoration: none;
                transition: color 0.3s ease;
            }
            .table-of-contents a:hover {
                color: #8b7355;
            }
        `;
        document.head.appendChild(style);
    };

    createTableOfContents();
});