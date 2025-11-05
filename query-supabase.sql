-- Query Supabase Database to see what's there

-- 1. Show all courses
SELECT "CourseId", "Title", "UnitCode", "Status", "CreatedAt"
FROM "Courses"
ORDER BY "CreatedAt" DESC;

-- 2. Count modules per course
SELECT c."UnitCode", c."Title", COUNT(m."ModuleId") as ModuleCount
FROM "Courses" c
LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId"
GROUP BY c."CourseId", c."UnitCode", c."Title";

-- 3. Count lessons per course
SELECT c."UnitCode", c."Title", COUNT(l."LessonId") as LessonCount
FROM "Courses" c
LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId"
LEFT JOIN "Lessons" l ON m."ModuleId" = l."ModuleId"
GROUP BY c."CourseId", c."UnitCode", c."Title";

-- 4. Check lesson media (images/3D models)
SELECT c."UnitCode", c."Title", COUNT(lm."MediaId") as MediaCount
FROM "Courses" c
LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId"
LEFT JOIN "Lessons" l ON m."ModuleId" = l."ModuleId"
LEFT JOIN "LessonMedia" lm ON l."LessonId" = lm."LessonId"
GROUP BY c."CourseId", c."UnitCode", c."Title";

-- 5. Show sample media paths (to see if they're local paths or Supabase Storage URLs)
SELECT "MediaId", "LessonId", "FilePath", "MediaType", "DisplayOrder"
FROM "LessonMedia"
ORDER BY "MediaId"
LIMIT 20;

-- 6. Check for forklift course specifically
SELECT * FROM "Courses" WHERE "UnitCode" = 'TLILIC003';

-- 7. Show forklift modules
SELECT m."ModuleId", m."Title", m."OrderIndex", m."DurationMinutes"
FROM "Modules" m
JOIN "Courses" c ON m."CourseId" = c."CourseId"
WHERE c."UnitCode" = 'TLILIC003'
ORDER BY m."OrderIndex";
