# DateBook

a dating app made with .net maui and .net webapi

DATING APP MATCH LOGIC SUMMARY

1. Quiz System:
- 5 questions, 20% each
- Min 60% needed to match
- Quiz answers stored in Quiz table

2. Finding Matches Flow:
- Get all users matching basic filters (age, gender)
- Calculate compatibility % on-the-fly for each user
- Only show users with ≥60% compatibility
- No pre-stored scores needed

3. Match Creation:
- User A likes User B
- If User B already liked User A:
   * Calculate final match %
   * Create Match record
   * Store that % in Match table
   * Show "It's a Match!"

4. Database:
- Quiz answers stored in Quiz table
- Match % only stored when match happens
- No pre-calculated scores stored

5. Stack Logic:
- Calculate scores real-time
- Filter before showing users
- Only compatible users appear in stack
