import { SimpleClaim } from "./simple-claim";
import { UserProfile } from "./user-profile";

export class AuthContext {
    userProfile: UserProfile;
    claims: SimpleClaim[];
  }