export class AppUserAuth{
    userName: string = "";
    bearerToken: string = "";
    isAuthenticated: boolean = false;
    canAccessPaymentDetails: boolean = false;
    canAddPaymentDetails: boolean = false;
    canUpdatePaymentDetails: boolean = false;

    canAccessOrders: boolean = false;
    canAddOrders: boolean = false;
    
    canAccessMenuItems: boolean = false;
    canAddMenuItems: boolean = false;
    // canAccessPaymentDetails: boolean = false;
    // canAccessPaymentDetails: boolean = false;
    // canAccessPaymentDetails: boolean = false;
    // canAccessPaymentDetails: boolean = false;

}