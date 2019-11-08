"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var GardenSocietyService = /** @class */ (function () {
    function GardenSocietyService(http) {
        this.http = http;
        this.requestsUrl = 'api/garden-society';
    }
    GardenSocietyService.prototype.get = function () {
        return this.http.get("" + this.requestsUrl, { withCredentials: true });
    };
    return GardenSocietyService;
}());
exports.GardenSocietyService = GardenSocietyService;
//# sourceMappingURL=GardenSocietyService.js.map