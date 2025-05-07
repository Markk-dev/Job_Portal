document.addEventListener("DOMContentLoaded", function () {
  document.querySelectorAll(".comment-button").forEach((button) => {
    button.addEventListener("click", function () {
      const postId = this.getAttribute("data-post-id");
      const author = this.getAttribute("data-author");
      const content = this.getAttribute("data-content");
      const likes = this.getAttribute("data-likes");
      const comments = this.getAttribute("data-comment-count");

      openCommentModal(postId, author, content, likes, comments);
    });
  });

  document
    .querySelector("#commentModal .close-button")
    .addEventListener("click", function () {
      closeCommentModal();
    });

  const commentTextarea = document.getElementById("modalCommentContent");
  if (commentTextarea) {
    commentTextarea.addEventListener("input", function () {
      this.style.height = "auto";
      this.style.height =
        (this.scrollHeight > 120 ? 120 : this.scrollHeight) + "px";
    });
  }

  // EDIT POST
  document.querySelectorAll(".edit-button").forEach((button) => {
    button.addEventListener("click", function () {
      const postId = this.getAttribute("data-post-id");
      let content = this.getAttribute("data-post-content");
      content = content.replace(" [Edited]", "");

      document.getElementById("editPostId").value = postId;
      document.getElementById("editPostContent").value = content;

      openEditModal();
    });
  });

  document
    .getElementById("closeEditModal")
    .addEventListener("click", function () {
      closeEditModal();
    });

  const editTextarea = document.getElementById("editPostContent");
  if (editTextarea) {
    editTextarea.addEventListener("input", function () {
      this.style.height = "auto";
      const newHeight = this.scrollHeight;
      this.style.height = (newHeight > 200 ? 200 : newHeight) + "px";
    });
  }

  // CLOSE MODAL
  window.addEventListener("click", function (event) {
    const commentModal = document.getElementById("commentModal");
    const editModal = document.getElementById("editPostModal");

    if (event.target == commentModal) closeCommentModal();
    if (event.target == editModal) closeEditModal();
  });

  // DELETE CONFIRMATION
  document.querySelectorAll(".delete-form").forEach((form) => {
    form.addEventListener("submit", function (event) {
      if (
        !confirm(
          "Are you sure you want to delete this post? This action cannot be undone."
        )
      ) {
        event.preventDefault();
      }
    });
  });

  //SEARCH FUNCTIONALITY
  const searchInput = document.querySelector(".search-box input");
  if (searchInput) {
    searchInput.addEventListener("input", function () {
      const searchTerm = this.value.toLowerCase();
      const posts = document.querySelectorAll(".post-item");

      posts.forEach((post) => {
        const postContent = post
          .querySelector(".post-text")
          .textContent.toLowerCase();
        const authorName = post
          .querySelector(".post-author")
          .textContent.toLowerCase();
        post.style.display =
          postContent.includes(searchTerm) || authorName.includes(searchTerm)
            ? ""
            : "none";
      });
    });
  }

  // APPLY MATERIAL ICONS TO BUTTONS
  updateActionButtons();

  document.querySelectorAll(".edit-comment-btn").forEach((btn) => {
    btn.addEventListener("click", function () {
      const commentId = this.getAttribute("data-comment-id");
      document.getElementById("edit-comment-form-" + commentId).style.display =
        "block";
    });
  });
  document.querySelectorAll(".cancel-edit-btn").forEach((btn) => {
    btn.addEventListener("click", function () {
      const commentId = this.getAttribute("data-comment-id");
      document.getElementById("edit-comment-form-" + commentId).style.display =
        "none";
    });
  });

  // Add these event listeners in the DOMContentLoaded event
  document.querySelectorAll(".delete-comment-form").forEach((form) => {
    form.addEventListener("submit", function (event) {
      if (
        !confirm(
          "Are you sure you want to delete this comment? This action cannot be undone."
        )
      ) {
        event.preventDefault();
      }
    });
  });
});

// MODAL HELPER FUNCTIONS
function openCommentModal(
  postId,
  authorName,
  postContent,
  likeCount,
  commentCount
) {
  const modal = document.getElementById("commentModal");
  document.getElementById("modalPostAuthor").textContent = authorName;
  document.getElementById("modalAuthorInitial").textContent =
    authorName.charAt(0);
  document.getElementById("modalPostContent").textContent = postContent;
  document.getElementById("modalLikeCount").textContent = likeCount;
  document.getElementById("modalCommentCount").textContent = commentCount;
  document.getElementById("modalPostId").value = postId;

  fetchAndDisplayComments(postId);
  modal.style.display = "flex";
  modal.classList.add("active");
}

function closeCommentModal() {
  const modal = document.getElementById("commentModal");
  modal.classList.remove("active");
  modal.style.display = "none";
}
function openEditModal() {
  const modal = document.getElementById("editPostModal");
  modal.style.display = "flex";

  setTimeout(() => {
    modal.classList.add("active");
  }, 10);

  setTimeout(() => {
    const textarea = document.getElementById("editPostContent");
    textarea.focus();
    textarea.style.height = "auto";
    textarea.style.height =
      (textarea.scrollHeight > 200 ? 200 : textarea.scrollHeight) + "px";
  }, 300);
}

function closeEditModal() {
  const modal = document.getElementById("editPostModal");
  modal.classList.remove("active");

  setTimeout(() => {
    modal.style.display = "none";
  }, 300);
}

// DISPLAY COMMENTS IN MODAl
function fetchAndDisplayComments(postId) {
  const commentsContainer = document.getElementById("modalComments");
  const commentsSection = document.getElementById(`comments-${postId}`);

  if (!commentsSection) {
    commentsContainer.innerHTML =
      '<div class="empty-comments">Be the first to comment!</div>';
    return;
  }

  const commentItems = commentsSection.querySelectorAll(".comment-item");
  const count = commentItems.length;
  document.getElementById("modalCommentCount").textContent = count;

  if (count === 0) {
    commentsContainer.innerHTML =
      '<div class="empty-comments">Be the first to comment!</div>';
    return;
  }

  // Get the anti-forgery token from the page
  const token = document.querySelector(
    'input[name="__RequestVerificationToken"]'
  ).value;

  commentsContainer.innerHTML = "";
  commentItems.forEach((item) => {
    const author = item.querySelector(".comment-author").textContent;
    const content = item.querySelector(".comment-content").textContent;
    const commentId =
      item
        .querySelector(".edit-comment-btn")
        ?.getAttribute("data-comment-id") || "";

    commentsContainer.innerHTML += `
    <div class="modal-comment" id="modal-comment-${commentId}">
        <div class="comment-avatar">
            <span class="avatar-initial">${author.charAt(0)}</span>
        </div>
        <div class="comment-content">
            <div class="comment-header">
                <span class="comment-author">${author}</span>
                <time class="comment-time">Just now</time>
                <button type="button" class="action-button edit-comment-btn" 
                    data-comment-id="${commentId}" 
                    data-comment-content="${content}"
                    title="Edit Comment">
                    <i class="material-icons">edit</i>
                </button>
                <form method="post" action="?handler=DeleteComment&commentId=${commentId}" class="delete-comment-form">
                    <input type="hidden" name="__RequestVerificationToken" value="${token}" />
                    <button type="submit" class="action-button delete-comment-btn" title="Delete Comment">
                        <i class="material-icons">delete_outline</i>
                    </button>
                </form>
            </div>
            <p class="comment-text">${content}</p>
        </div>
    </div>
    <div class="edit-comment-form" id="edit-comment-form-${commentId}" style="display:none;">
        <form method="post" action="?handler=EditComment&commentId=${commentId}">
            <input type="hidden" name="__RequestVerificationToken" value="${token}" />
            <textarea name="EditCommentContent" required>${content}</textarea>
            <button type="submit" class="action-button" title="Save">
                <i class="material-icons">check</i>
            </button>
            <button type="button" class="action-button cancel-edit-btn" data-comment-id="${commentId}" title="Cancel">
                <i class="material-icons">close</i>
            </button>
        </form>
    </div>
    `;
  });

  // Re-attach event listeners for edit/cancel buttons
  document.querySelectorAll(".edit-comment-btn").forEach((btn) => {
    btn.addEventListener("click", function () {
      const commentId = this.getAttribute("data-comment-id");
      document.getElementById("edit-comment-form-" + commentId).style.display =
        "block";
    });
  });
  document.querySelectorAll(".cancel-edit-btn").forEach((btn) => {
    btn.addEventListener("click", function () {
      const commentId = this.getAttribute("data-comment-id");
      document.getElementById("edit-comment-form-" + commentId).style.display =
        "none";
    });
  });
}

//UPDATE ACTION BUTTONS
function updateActionButtons() {
  document.querySelectorAll(".like-button").forEach((button) => {
    const count = button.querySelector(".count")?.textContent || "0";
    button.innerHTML = `<i class="material-icons">favorite</i> <span class="count">${count}</span>`;
  });

  document.querySelectorAll(".comment-button").forEach((button) => {
    const count = button.textContent.match(/\d+/);
    button.innerHTML = `<i class="material-icons">chat_bubble_outline</i> ${
      count ? count[0] : "0"
    }`;
  });

  document.querySelectorAll(".edit-button").forEach((button) => {
    button.innerHTML = `<i class="material-icons">edit</i>`;
  });

  document.querySelectorAll(".delete-button").forEach((button) => {
    button.innerHTML = `<i class="material-icons">delete_outline</i>`;
  });
}
