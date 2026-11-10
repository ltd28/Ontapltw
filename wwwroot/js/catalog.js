(function () {
    const container = document.getElementById('product-list');
    const heading = document.getElementById('product-heading');
    if (!container) return;

    document.addEventListener('click', async function (event) {
        const link = event.target.closest('a.category-filter');
        if (!link) return;
        event.preventDefault();
        try {
            const response = await fetch(link.href, { headers: { 'X-Requested-With': 'XMLHttpRequest' } });
            if (!response.ok) throw new Error('Không tải được sản phẩm.');
            container.innerHTML = await response.text();
            if (heading) heading.textContent = link.textContent.trim().toUpperCase();
            document.querySelectorAll('.menu li').forEach(li => li.classList.remove('active'));
            link.closest('li')?.classList.add('active');
        } catch (error) {
            console.error(error);
            window.location.href = link.href;
        }
    });
})();
