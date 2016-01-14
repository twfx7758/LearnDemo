//一段普通文本被点击就变成一个可编辑的表单域，
//以便用户就地对这段文本进行编辑
function EditInPlaceField(id, parent, value) {
    this.id = id;
    this.value = value || "default value";
    this.parentElement = parent;

    this.CreateElement(this.id);
    this.attachEvents();
}

EditInPlaceField.prototype = {
    CreateElement: function (id) {
        this.containerElement = document.createElement("div");
        this.parentElement.appendChild(this.containerElement);

        this.staticElement = document.createElement("span");
        this.containerElement.appendChild(this.staticElement);
        this.staticElement.innerHTML = this.value;

        this.fieldElement = document.createElement("input");
        this.fieldElement.type = "text";
        this.fieldElement.value = this.value;
        this.containerElement.appendChild(this.fieldElement);

        this.saveButton = document.createElement("input");
        this.saveButton.type = "button";
        this.saveButton.value = "Save";
        this.containerElement.appendChild(this.saveButton);

        this.cancelButton = document.createElement("input");
        this.cancelButton.type = "button";
        this.cancelButton.value = "Cancel";
        this.containerElement.appendChild(this.cancelButton);

        this.convertToText();
    },

    attachEvents: function () {
        var that = this;
        this.staticElement.addEventListener("click", function () { that.convertToEditable(); }, false);
        this.saveButton.addEventListener("click", function () { that.Save(); }, false);
        this.cancelButton.addEventListener("click", function () { that.cancel(); }, false);
    },

    convertToText: function () {
        this.fieldElement.style.display = "none";
        this.saveButton.style.display = "none";
        this.cancelButton.style.display = "none";
        this.staticElement.style.display = "inline";

        this.setValue(this.value);
    },

    convertToEditable: function () {
        this.fieldElement.style.display = "inline";
        this.saveButton.style.display = "inline";
        this.cancelButton.style.display = "inline";
        this.staticElement.style.display = "none";
    },

    Save: function () {
        this.value = this.getValue();
        var that = this;

        that.convertToText();

        //var callback = {
        //    success: function () { that.convertToText(); },
        //    failure: function () { alert("Error saving value."); }
        //};
    },

    cancel: function () {
        this.convertToText();
    },


    setValue: function (value) {
        this.fieldElement.value = value;
        this.staticElement.innerHTML = value;
    },

    getValue: function () {
        return this.fieldElement.value;
    }
};