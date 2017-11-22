(function () {
    try {
        keys = Object.keys(Sys.Application._components);
        containsAtLeastOneCommandContainer = false;
        for (i = 0; i < keys.length; i++) {
            if (keys[i].startsWith('commandContainer') || keys[i].startsWith('crmRibbonData')) {
                containsAtLeastOneCommandContainer = true;
                key = keys[i];
                component = Sys.Application._components[key];
                isComponentInitialized = component.get_isInitialized();
                isUpdating = component.get_isUpdating();
                if (isComponentInitialized !== true || isUpdating !== false) {
                    return false;
                }
            }
        }
        return containsAtLeastOneCommandContainer;
    }
    catch (e) {
        console.log('G1ANT: Exception occured while trying to check command bars initialization status. Sys.Application._components namespace is probably not loaded yet,');
        return false;
    }
})();
