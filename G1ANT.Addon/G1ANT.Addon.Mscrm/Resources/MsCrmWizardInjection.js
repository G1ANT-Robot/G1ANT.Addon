$(document).ready(function () {
    console.log("g1ant script injected on " + (new Date()).format("dd/MM/yyyy hh:mm:ss"));
    function elementClicked(e) {
        var elementIsCrmField = false;
        var mscrmElement = getMscrmSetValueElement(e.target);
        var mscrmFilter = 'id';
        if (mscrmElement) {
            elementIsCrmField = mscrmElement.action === 'setvalue';
            mscrmElement = mscrmElement.element;
        }
        else {
            var mscrmClickItem = getMscrmClickedElement(e.target);
            if (mscrmClickItem) {
                mscrmElement = mscrmClickItem.element;
                mscrmFilter = mscrmClickItem.by;
            }
        }

        if (mscrmElement) {
            var element = window.document.createElement('Div');
            element.setAttribute('id', 'MsCrmWizard');
            element.setAttribute('class', 'G1antMsCrmWizardClass');
            element.setAttribute('style', 'display:none');
            element.setAttribute('target_name', mscrmElement.tagName);
            var currentIframe = getIframe(mscrmElement);
            if (currentIframe) {
                var id2 = currentIframe.id ? currentIframe.id : '';
                element.setAttribute('target_iframe_id', id2);
                var title2 = currentIframe.getAttribute('title') ? currentIframe.getAttribute('title') : '';
                element.setAttribute('target_iframe_title', title2);
                var name2 = currentIframe.name ? currentIframe.name : '';
                element.setAttribute('target_iframe_title', name2);
            }
            if (mscrmFilter === 'id') {
                element.setAttribute('target_id', mscrmElement.id);
            }
            else if (mscrmFilter === 'class') {
                element.setAttribute('target_class', mscrmElement.className);
            }
            element.setAttribute('data-element-type', elementIsCrmField ? 'setvalue' : 'click');
            window.document.body.appendChild(element);
            console.log((new Date()).format("dd/MM/yyyy hh:mm:ss") + ": Element added. " +
                "Action: " + element.getAttribute('data-element-type') + " " +
                "Id: " + element.getAttribute('target_id') + " " +
                "Class: " + element.getAttribute('target_class'));
            inject();
        }
    }

    function getIframe(element) {
        try {
            return element.ownerDocument.defaultView.frameElement;
        }
        catch (e) {
            return null;
        }
    }

    function getMscrmSetValueElement(element) {
        /*debugger;*/
        while (
            (
                !element.id || // element needs an id to be mscrm.setvalue element
                element.tagName !== 'DIV' || // element needs to be div
                element.getAttribute('role') === 'list' // subject tree exception
            ) && element.parentElement
        ) {
            if (element.id && (element.getAttribute("src") === "/_imgs/search_normal.gif" || element.id.endsWith("_lookupSearchIcon") ||
                element.getAttribute("src") === "/_imgs/search_hover.gif")) {
                return null; // search magnifier cliecked, we should not set it using mscrm.setvalue
            }
            element = element.parentElement;
        }
        if (element.id && (element.getAttribute("src") === "/_imgs/search_normal.gif" ||
            element.id.endsWith("_lookupSearchIcon") ||
            element.getAttribute("src") === "/_imgs/search_hover.gif")) {

            return null; // search magnifier cliecked, we should not set it using mscrm.setvalue
        }
        var containsNeededClass = false;
        for (var i = 0; i < element.classList.length; i++) {
            var elClass = element.classList[i];
            if (elClass === 'lookup' ||
                elClass === 'ms-crm-Lookup' ||
                elClass === 'ms-crm-Inline-Value' ||
                elClass === 'ms-crm-Inline-Lookup' ||
                elClass === 'picklist' ||
                elClass === 'ms-crm-Inline-Chrome') {
                containsNeededClass = true;
                break;
            }
        }
        element = (element.id && element.tagName === 'DIV' && containsNeededClass) ? element : null;
        if (!element) {
            return null;
        }
        else if (element.getAttribute("hascompositedata") !== "true") {
            return { element: element, action: 'setvalue' }
        }
        else {
            return { element: element, action: 'click' }
        }
    }

    function getMscrmClickedElement(element) {
        while (!element.id && element.parentElement) {
            if (element.className && element.className === 'ms-crm-InlineLookup-FooterSection-AddAnchor') {
                return { by: 'class', element: element }
            }
            element = element.parentElement;
        }
        return (element.id) ? { by: 'id', element: element } : null;
    }

    function inject() {
        var allFrames = [];

        function recursFrames(context) {
            try {
                for (var i = 0; i < context.frames.length; i++) {
                    try { allFrames.push(context.frames[i]); } catch (e) { /*debugger;*/ }
                    try { recursFrames(context.frames[i]); } catch (e) { /*debugger;*/ }
                }
            }
            catch (e) { /*debugger;*/ }
        }
        recursFrames(window);
        allFrames.push(window);
        for (var i = 0; i < allFrames.length; i++) {
            try {
                var currentFrameElements = allFrames[i].document.querySelectorAll(':not([g1antRecorderInjected])');
                for (var j = 0; j < currentFrameElements.length; j++) {
                    try {
                        var currentElement = currentFrameElements[j];
                        if (currentElement.id ||
                            (currentElement.className &&
                                (currentElement.className === 'ms-crm-InlineLookup-FooterSection-AddAnchor') // lookup dialog window, 'new' link at the bottom
                            )) {
                            if (currentElement.getAttribute('g1antRecorderInjected') !== 'true') {
                                var tempFunction = null;
                                if (currentElement.tagName.toLowerCase() !== 'button') {

                                    if (currentElement.className === 'navButtonLink') {
                                        try {
                                            currentElement.onclick = function (e) { elementClicked(e); this.ownerDocument.defaultView.DXTools.QuickView.Menu.LoadUrl(this); return false; }
                                        } catch (e) { /*debugger;*/ }
                                    } else

                                        if (currentElement.onclick && currentElement.id) {

                                            tempFunction = currentElement.onclick;
                                            currentElement.onclick = function (e) { elementClicked(e); tempFunction(e); return false; }
                                        }
                                        else {
                                            currentElement.onclick = elementClicked;
                                        }
                                }
                                if (!currentElement.onfocus && currentElement.tagName.toLowerCase() === 'button') {
                                    currentElement.onmousedown = function (e) {
                                        e.target.setAttribute('g1antHoveredMouseDown', 'true');
                                    }
                                    currentElement.onmouseup = function (e) {
                                        e.target.setAttribute('g1antHoveredMouseDown', '');
                                    }
                                    currentElement.onmouseover = function (e) {
                                        e.target.setAttribute('g1antHovered', 'true');
                                    }
                                    currentElement.onmouseleave = function (e) {
                                        e.target.setAttribute('g1antHovered', '');
                                    }
                                    currentElement.onfocus = function (e) {
                                        if (e.target.getAttribute('g1antHovered') === 'true' &&
                                            e.target.getAttribute('g1antHoveredMouseDown') === 'true') {
                                            elementClicked(e);
                                        }
                                    }
                                }
                                currentElement.setAttribute('g1antRecorderInjected', 'true');
                            }
                        }
                    } catch (e) { /*debugger;*/ }
                }
            } catch (e) { /*debugger;*/ }
        }
    }
    inject();
});