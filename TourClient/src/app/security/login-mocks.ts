import { AppUserAuth } from "./app-user-auth";

export const LOGIN_MOCKS: AppUserAuth[] = [
    {
        userName: "jsaldana",
        bearerToken: "asdfqwerlkjhpoiu1234",
        isAuthenticated: true,
        canAccessPaymentDetails: true,
        canAddPaymentDetails: true,
        canUpdatePaymentDetails: false
    },
    {
        userName: "prodriguez",
        bearerToken: "udjehfyyudgheycgey736",
        isAuthenticated: true,
        canAccessPaymentDetails: false,
        canAddPaymentDetails: true,
        canUpdatePaymentDetails: false
    }
];
