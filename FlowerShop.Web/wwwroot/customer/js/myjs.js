document
  .querySelectorAll(".dropdown-submenu > .nav-link")
  .forEach(function (element) {
    element.addEventListener("click", function (e) {
      var parent = this.parentElement;
      parent.classList.toggle("open");
      e.stopPropagation();
      e.preventDefault();
    });
  });
