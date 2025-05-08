# Online-Crouse-Tracker

- Flow Chart Link: https://app.eraser.io/workspace/E9eoJFT63SPX4PUTtzmd

# To create a flowchart for the "OnlineCourseTracker" system, we'll represent the major steps and decision points in the program's logic. Here's a description of the flow:

Flowchart Description:
Start: The program begins.

## Main Menu: The user is presented with the main menu with the following options:

1. Register as Student

2. Login as Student

3. Admin Dashboard

4. Exit

### Option 1 - Register as Student:

- Input: Student's username and password.

- Action: A new Student object is created, and the student is added to the system.

- Output: Registration is successful.

### Option 2 - Login as Student:

- Input: Student's username and password.

- Action: Check if the student credentials match the registered users.

- If valid: Proceed to the student dashboard.

- If invalid: Show "Login failed."

### Student Dashboard: After a successful login, the student can:

1. View All Courses

2. Enroll in Course

3. Update Progress

4. My Dashboard

5. Logout

### Option 1 - View All Courses:

- Action: Display a list of all available courses.

### Option 2 - Enroll in Course:

- Input: Course title.

### Action: Check if the course exists. If found, enroll the student in the course; if not, show "Course not found."

### Option 3 - Update Progress:

- Input: Course title and progress percentage.

### Action: Check if the student is enrolled in the course. If enrolled, update the progress.

### Option 4 - My Dashboard:

- Action: Show a summary of the student's enrolled courses and progress.

### Option 5 - Logout:

- Action: Log the student out and return to the main menu.

### Option 3 - Admin Dashboard:

- Input: Admin username and password.

### Action: Verify admin credentials.

- If valid: Admin can view all students' progress.

- If invalid: Show "Invalid admin credentials."

Exit: End the program.
