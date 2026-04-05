export interface UserProfile {
    id: string;
    userName: string;
    name: string | null;
    email: string;
    profilePictureUrl: string | null;
    role: string;
}

export interface UpdateProfileRequest {
    name?: string;
    email?: string;
    currentPassword?: string;
    newPassword?: string;
}
