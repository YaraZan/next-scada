type UserRole = 'root' | 'user';
type Theme = 'dark' | 'light';
type Language = 'en' | 'fr' | 'es' | 'de' | 'zh' | 'jp';

interface UserSettings {
  theme: Theme;
  language: Language;
}

export interface User {
  name: string;
  email: string;
  role: UserRole;
  settings: UserSettings;
}
