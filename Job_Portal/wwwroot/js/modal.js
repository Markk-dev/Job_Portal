document.addEventListener('DOMContentLoaded', function () {
    const modalOverlay = document.getElementById('comment-modal-overlay');
    const closeBtn = document.querySelector('.close-btn');
    const cancelBtn = document.querySelector('.modal-cancel-btn');
    const commentsSection = document.getElementById('modal-comments');
    const likeCount = document.getElementById('modal-like-count');
    const commentCount = document.getElementById('modal-comment-count');
    const textarea = document.getElementById('modal-comment-textarea');

    // Toggle post inline comments (below post)
    document.querySelectorAll('.comment-button').forEach(button => {
        button.addEventListener('click', function (event) {
            const postId = button.getAttribute('data-post-id');
            const commentsSectionInline = document.getElementById('comments-' + postId);

            // If Shift key is held, open modal instead of toggling inline
            if (event.shiftKey) {
                const comments = JSON.parse(button.dataset.comments || '[]');
                const author = button.dataset.author || 'User';
                const likes = button.dataset.likeCount || 0;
                const commentTotal = button.dataset.commentCount || 0;

                likeCount.innerText = likes;
                commentCount.innerText = commentTotal;
                textarea.value = '';

                commentsSection.innerHTML = comments.map(comment =>
                    `<div class="modal-comment">
                        <div class="modal-comment-avatar"><i class="fas fa-user"></i></div>
                        <div class="modal-comment-content">
                            <div class="modal-comment-header">
                                <div class="modal-comment-author">${comment.AuthorName}</div>
                                <div class="modal-comment-time">Just now</div>
                                <div class="modal-comment-menu"><i class="fas fa-ellipsis-h"></i></div>
                            </div>
                            <div class="modal-comment-text">${comment.Content}</div>
                            <div class="modal-comment-actions">
                                <button class="modal-action-btn"><i class="far fa-heart"></i> Like</button>
                                <button class="modal-action-btn"><i class="far fa-comment"></i> Reply</button>
                                <button class="modal-action-btn"><i class="fas fa-share"></i> Share</button>
                            </div>
                        </div>
                    </div>`).join('');

                // Make sure the modal overlay is visible
                modalOverlay.style.display = 'flex';
                modalOverlay.classList.add('show'); // Add a class for smooth transitions (optional)
            } else {
                // Toggle inline comments
                commentsSectionInline.style.display =
                    commentsSectionInline.style.display === 'none' ? 'block' : 'none';
            }
        });
    });

    // Close modal when clicking close or cancel
    [closeBtn, cancelBtn].forEach(btn => {
        if (btn) {
            btn.addEventListener('click', () => {
                modalOverlay.style.display = 'none';
                modalOverlay.classList.remove('show'); // Remove the transition class
            });
        }
    });
});
