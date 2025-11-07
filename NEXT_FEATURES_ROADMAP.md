# RTO Web LMS - Next Features Roadmap

## ✅ What You Already Have

### Core Functionality
- ✅ **User System** - Login, Register, User Roles (Student, Instructor, Admin)
- ✅ **Course Management** - Courses, Modules, Lessons
- ✅ **Enrollment System** - Students can enroll in courses
- ✅ **Progress Tracking** - Lesson completion tracking
- ✅ **Certificate Generation** - Issue certificates on completion
- ✅ **3D Interactive Content** - Model viewer for forklift training
- ✅ **Content Editor** - Admin page to edit lesson content
- ✅ **Image Galleries** - Lesson media with slideshow

### Compliance (Partial)
- ✅ **AVETMISS Fields** - User model has all required AVETMISS data fields:
  - USI (Unique Student Identifier)
  - Demographics (DOB, Gender, Indigenous Status)
  - Education (Prior education, school level)
  - Employment (Labor force status)
  - Language & Disability
  - Contact details

### Assessment
- ✅ **Quiz System** - Database models for:
  - Quizzes (with passing scores, time limits, max attempts)
  - Questions and Answers
  - Quiz Attempts tracking
- ⚠️ **NOT YET**: Quiz builder interface

### Audit & Security
- ✅ **Audit Logging** - Track all important actions
- ✅ **Database** - PostgreSQL (production) + SQLite (dev)

---

## 🚀 What to Build Next (Priority Order)

### **PRIORITY 1: Complete Authentication & Admin**

#### 1.1 Proper Admin Login
**Status:** Login page exists but needs role-based access

**What's Needed:**
- Admin dashboard with restricted access
- Role-based permissions (prevent students from accessing admin pages)
- Session management
- "Remember me" functionality

**Implementation:**
- Create admin-only routes
- Add authentication middleware
- Build admin dashboard with:
  - User management
  - Course management
  - Reports overview
  - System settings

---

#### 1.2 Enhanced Registration
**Status:** Basic registration works

**What's Needed:**
- Collect AVETMISS data during sign-up
- Multi-step registration form
- Email verification (optional)
- USI validation

**Forms to Add:**
1. **Step 1:** Basic Info (Name, Email, Password)
2. **Step 2:** Personal Details (DOB, Gender, Address)
3. **Step 3:** Education & Employment
4. **Step 4:** USI & Indigenous Status
5. **Step 5:** Review & Submit

---

### **PRIORITY 2: Quiz Builder & Assessment**

#### 2.1 Quiz Creator (Admin)
**Status:** Database ready, no interface yet

**What's Needed:**
- Create quiz from admin panel
- Add questions with multiple choices
- Set correct answers
- Configure passing score, time limit
- Assign quiz to lessons

**Pages to Build:**
- `/admin/quizzes` - List all quizzes
- `/admin/quiz/create` - Create new quiz
- `/admin/quiz/edit/{id}` - Edit existing quiz
- Quiz question editor with drag-and-drop ordering

---

#### 2.2 Quiz Taking (Student)
**Status:** QuizPage.razor exists but needs completion

**What's Needed:**
- Display quiz questions
- Timer countdown
- Submit answers
- Show results with feedback
- Track attempts (limit to max attempts)
- Record in QuizAttempt table

---

### **PRIORITY 3: ASQA Compliance Features**

#### 3.1 Training & Assessment Strategy (TAS)
**What's Needed:**
- Document how training is delivered
- Assessment methods for each unit
- Resource requirements
- Validation strategy

**Implementation:**
- Add TAS document upload per course
- Store training plan details
- Link assessments to competencies

---

#### 3.2 Learning Resources
**What's Needed:**
- Learner guide for each unit
- Assessment tools
- Trainer/Assessor guide
- Mapping documents

**Implementation:**
- Upload documents per lesson/module
- Version control for documents
- Student access to learning resources

---

#### 3.3 Validation & Moderation
**What's Needed:**
- Validation strategy
- Sampled student work
- Moderation reports
- Assessor feedback

**Implementation:**
- Assessment review workflow
- Moderator role
- Validation schedule tracking

---

#### 3.4 Student Management Requirements
**What's Needed:**
- Pre-enrollment information
- LLN (Language, Literacy, Numeracy) assessment
- Recognition of Prior Learning (RPL)
- Unique Student Identifier (USI) verification

**Implementation:**
- Pre-course questionnaire
- LLN test module
- RPL application form
- USI verification API integration

---

### **PRIORITY 4: AVETMISS Reporting**

#### 4.1 AVETMISS Export
**Status:** Fields exist in database

**What's Needed:**
- Export student data in NAT files format:
  - NAT00010 - Training Organisation
  - NAT00020 - Training Organisation Delivery Location
  - NAT00030 - Course
  - NAT00060 - Qualification Completed
  - NAT00080 - Module/Unit Completed
  - NAT00085 - Enrolment
  - NAT00090 - Disability
  - NAT00100 - Prior Educational Achievement
  - NAT00120 - Client
  - NAT00130 - Client Postal Details

**Implementation:**
- Create AVETMISS export service
- Generate .txt files in correct format
- Validation against AVETMISS rules
- Export UI at `/admin/avetmiss/export`

---

#### 4.2 Data Validation
**What's Needed:**
- Validate all AVETMISS fields
- Check for missing required data
- Flag incomplete student records
- Validation reports

---

### **PRIORITY 5: training.gov.au Integration**

#### 5.1 Unit of Competency Import
**What's Needed:**
- Import units from training.gov.au
- Auto-populate:
  - Unit code (e.g., TLILIC0003)
  - Unit title
  - Core/Elective status
  - Nominal hours
  - Elements and performance criteria

**Implementation:**
- training.gov.au API integration
- Search units by code
- Auto-create course structure
- Keep units up to date

---

#### 5.2 Qualification Mapping
**What's Needed:**
- Map qualifications to units
- Core units vs electives
- Packaging rules
- Prerequisites tracking

---

### **PRIORITY 6: Enhanced Student Features**

#### 6.1 Student Dashboard Improvements
- Learning pathway visualization
- Course progress charts
- Upcoming assessments
- Certificates earned
- USI display

---

#### 6.2 Communication
- Messages/notifications
- Assignment submissions
- Trainer feedback
- Discussion forums (optional)

---

#### 6.3 Calendar & Scheduling
- Training schedule
- Assessment due dates
- Class bookings (if face-to-face)
- Reminder notifications

---

### **PRIORITY 7: Reporting & Analytics**

#### 7.1 Student Reports
- Individual progress reports
- Transcript of results
- Attendance records (if applicable)
- Completion status

---

#### 7.2 Management Reports
- Enrollment statistics
- Completion rates
- Assessment outcomes
- Trainer workload
- Financial reports

---

#### 7.3 ASQA Audit Reports
- Student files checklist
- Compliance dashboard
- Document currency check
- Trainer qualifications tracking

---

## 📋 Implementation Checklist

### Phase 1: Foundation (Week 1-2)
- [ ] Admin authentication & dashboard
- [ ] Role-based access control
- [ ] Multi-step registration with AVETMISS data
- [ ] Quiz builder interface

### Phase 2: Assessment (Week 3-4)
- [ ] Quiz taking interface
- [ ] Assessment submission
- [ ] Results & feedback system
- [ ] RPL process

### Phase 3: Compliance (Week 5-6)
- [ ] AVETMISS export functionality
- [ ] Data validation reports
- [ ] USI verification
- [ ] LLN assessment

### Phase 4: Integration (Week 7-8)
- [ ] training.gov.au integration
- [ ] Unit import functionality
- [ ] Qualification mapping
- [ ] Document management

### Phase 5: Polish (Week 9-10)
- [ ] Enhanced dashboards
- [ ] Reporting suite
- [ ] Email notifications
- [ ] Mobile responsiveness

---

## 🛠️ Quick Start: What to Build First?

I recommend starting with:

### Option A: **Quiz System** (Most Requested)
Build the quiz creator and quiz-taking interface. This will make courses interactive immediately.

### Option B: **Admin Dashboard** (Most Important)
Secure the admin area and build proper user management first.

### Option C: **AVETMISS Export** (Compliance Critical)
If you need to report soon, build the AVETMISS export functionality.

---

## 💡 Which Would You Like to Build First?

Let me know which priority you'd like to tackle, and I can:
1. Create the pages/functionality
2. Set up the database changes (if needed)
3. Add the necessary features
4. Test and document it

**Quick Implementations Available:**
- 🎯 Quiz Builder (2-3 hours)
- 🔐 Admin Dashboard (1-2 hours)
- 📊 AVETMISS Export (3-4 hours)
- 📝 Multi-step Registration (2 hours)
- 🏆 Enhanced Student Dashboard (1 hour)

Just say what you want next!
