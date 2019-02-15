WatinSearchHelper = function () {
    var earTagId = 1;

    var getElementId = function (selector, searchType) {
        resultId = "_no_element_";
        element = null;
        switch (searchType) {
            case "query":
                elements = document.querySelectorAll(selector);
                if (elements.length > 0) {
                    element = elements[0];
                }
                break;
            case "jquery":
                elements = $(selector);
                if (elements.length > 0) {
                    element = elements[0];
                }
                break;
        }
        if (element) {
            if (!element.id) {
                element.id = "_watin_search_" + earTagId++;
            }
            resultId = element.id;
        }
        return resultId;
    };

    return { getElementId: getElementId };
}();


