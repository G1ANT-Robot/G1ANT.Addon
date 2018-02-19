WatinSearchHelper = function () {
    var earTagId = 1;

    var getElementId = function (cssSelector, iFrame) {
        currentFrame = getIframe(iFrame);
        var resultId = "_no_element_";
        var el = $(currentFrame).contents().find(cssSelector);
        if (el.length > 0) {
            var firstEl = el[0];
            if (!firstEl.id) {
                firstEl.id = "_watin_search_" + earTagId++;
            }
            resultId = firstEl.id;
        }
        return resultId;
    };

    var getAllIframes = function () {
        allFrames = [];

        function isFrameAlreadyAdded(frame) {
            for (i = 0; i < allFrames.length; i++) {
                if (allFrames[i] === frame) {
                    return true;
                }
            }
            return false;
        }

        function recursFrames(context) {
            console.log('recursFrames methods started, frames length: ' + context.frames.length);
            try {
                for (i = 0; i < context.frames.length; i++) {
                    permissionDeniedTest = context.frames[i].id;
                    try {
                        if (isFrameAlreadyAdded(context.frames[i]) === false) {
                            allFrames.push(context.frames[i]);
                            try {
                                if (context.frames.length > 1) {
                                    recursFrames(context.frames[i]);
                                }
                            } catch (e) { /*debugger;*/ }
                        }
                    } catch (e) { /*debugger;*/ }                    
                }
            }
            catch (e) { /*debugger;*/ }
        }
        recursFrames(window);
        allFrames.push(window);
        return allFrames;
    };

    var getIframe = function getJsFrame(name) {
        if (name === "none") {
            return window.document;
        }
        else if (name === "") {
            frames = getAllIframes();
            for (i = 0; i < frames.length; i++) {
                try {
                    if (frames[i].frameElement.style.visibility === "visible" &&
                        frames[i].frameElement.getAttribute('title') === "Content Area") {
                        return frames[i].document;
                    }
                }
                catch (e) {/* debugger; */ }
            }
        }
        else {
            frames = getAllIframes();
            for (i = 0; i < frames.length; i++) {
                try {
                    if (frames[i].frameElement.id === name) {
                        return frames[i].document;
                    }
                }
                catch (e) {/* debugger; */ }
            }
        }
        return null;
    };

    return { getElementId: getElementId };
}();


