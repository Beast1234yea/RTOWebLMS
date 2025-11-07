# Interactive Lesson Content Templates

This document contains ready-to-use HTML templates for adding interactive content to your RTO LMS lessons.

## How to Use

1. Go to http://localhost:8080/admin/edit-lesson
2. Select a lesson
3. Copy any template below
4. Paste it into the lesson content editor
5. Click "Save Changes"

You can mix and match multiple templates in the same lesson!

---

## 1. 3D Forklift Model

Interactive 3D model that students can rotate and zoom.

```html
<div style='margin: 20px 0; padding: 20px; border: 2px solid #007bff; border-radius: 10px; background: #f8f9fa;'>
    <h3 style='color: #007bff;'>🚛 Interactive 3D Forklift</h3>
    <model-viewer src='/models/forklift/FWS_Forklift_01_Blue.gltf' alt='Forklift' auto-rotate camera-controls environment-image='neutral' style='width: 100%; height: 500px; background: #e3f2fd; border-radius: 8px;'></model-viewer>
    <p style='margin-top: 10px; text-align: center; color: #666;'>🖱️ Drag to rotate • 🔍 Scroll to zoom</p>
</div>
```

---

## 2. Image with Caption

Display an image with a descriptive caption.

```html
<div style='margin: 20px 0; text-align: center;'>
    <img src='/images/forklift/page_001_image_01.jpeg' style='max-width: 100%; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.2);' />
    <p style='color: #666; font-style: italic; margin-top: 10px;'>Figure 1: Forklift safety diagram</p>
</div>
```

**Note:** Change the image path (`/images/forklift/page_001_image_01.jpeg`) to match your images.

---

## 3. Multiple Choice Quiz

Interactive quiz questions for testing knowledge.

```html
<div style='margin: 20px 0; padding: 20px; background: #fff3cd; border-left: 5px solid #ffc107; border-radius: 5px;'>
    <h4 style='color: #856404;'>📋 Safety Quiz</h4>
    <p><strong>What should you check before operating?</strong></p>
    <label style='display: block; margin: 8px 0;'><input type='radio' name='q1' value='a'> A) Nothing needed</label>
    <label style='display: block; margin: 8px 0;'><input type='radio' name='q1' value='b'> B) Brakes only</label>
    <label style='display: block; margin: 8px 0;'><input type='radio' name='q1' value='c'> C) All safety equipment ✓</label>
    <label style='display: block; margin: 8px 0;'><input type='radio' name='q1' value='d'> D) Just the horn</label>
</div>
```

**Tip:** Change `name='q1'` to `name='q2'`, `name='q3'`, etc. for additional questions.

---

## 4. Interactive Hotspot (Click on Image)

Clickable buttons on an image that reveal information.

```html
<div style='margin: 20px 0; padding: 20px; background: #e3f2fd; border-radius: 10px;'>
    <h4 style='color: #1976d2;'>🎯 Identify the Parts</h4>
    <p>Click on the forklift parts:</p>
    <div style='position: relative; max-width: 600px; margin: 0 auto;'>
        <img src='/images/forklift/page_001_image_01.jpeg' style='width: 100%; border-radius: 8px;' />
        <button onclick='alert("✅ Correct! This is the MAST")' style='position: absolute; top: 30%; left: 50%; width: 40px; height: 40px; border-radius: 50%; background: #ff9800; color: white; border: none; cursor: pointer; font-weight: bold;'>?</button>
        <button onclick='alert("✅ Correct! These are the FORKS")' style='position: absolute; top: 70%; left: 50%; width: 40px; height: 40px; border-radius: 50%; background: #2196f3; color: white; border: none; cursor: pointer; font-weight: bold;'>?</button>
    </div>
</div>
```

**Tip:** Adjust `top: 30%` and `left: 50%` to position buttons over different parts of your image.

---

## 5. Video Embed (YouTube)

Embed training videos from YouTube or other platforms.

```html
<div style='margin: 20px 0;'>
    <h4 style='color: #2196f3;'>📺 Training Video</h4>
    <div style='position: relative; padding-bottom: 56.25%; height: 0; overflow: hidden;'>
        <iframe src='https://www.youtube.com/embed/YOUR_VIDEO_ID' style='position: absolute; top: 0; left: 0; width: 100%; height: 100%; border: none; border-radius: 8px;'></iframe>
    </div>
</div>
```

**How to get YouTube video ID:**
1. Go to your YouTube video
2. Copy the URL: `https://www.youtube.com/watch?v=dQw4w9WgXcQ`
3. The ID is the part after `v=`: `dQw4w9WgXcQ`
4. Replace `YOUR_VIDEO_ID` with your ID

---

## 6. Important Warning Box

Highlight critical safety information.

```html
<div style='margin: 20px 0; padding: 15px; background: #ffebee; border-left: 5px solid #f44336; border-radius: 5px;'>
    <h4 style='color: #c62828; margin-top: 0;'>⚠️ SAFETY WARNING</h4>
    <p style='color: #b71c1c; margin-bottom: 0;'>Never operate a forklift without proper training and certification!</p>
</div>
```

**Variations:**
- Orange warning: Change `#ffebee` to `#fff3e0` and `#f44336` to `#ff9800`
- Blue info: Change `#ffebee` to `#e3f2fd` and `#f44336` to `#2196f3`

---

## 7. Pre-Operation Checklist

Interactive checklist students can tick off.

```html
<div style='margin: 20px 0; padding: 20px; background: #f1f8e9; border-radius: 8px;'>
    <h4 style='color: #558b2f;'>✅ Pre-Operation Checklist</h4>
    <label style='display: block; margin: 10px 0;'><input type='checkbox'> Check fuel/battery level</label>
    <label style='display: block; margin: 10px 0;'><input type='checkbox'> Test brakes</label>
    <label style='display: block; margin: 10px 0;'><input type='checkbox'> Check horn and lights</label>
    <label style='display: block; margin: 10px 0;'><input type='checkbox'> Inspect forks and mast</label>
    <label style='display: block; margin: 10px 0;'><input type='checkbox'> Check tire pressure</label>
</div>
```

---

## 8. Image Gallery

Display multiple images in a grid layout.

```html
<div style='margin: 20px 0; padding: 20px; background: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
    <h4 style='color: #9c27b0;'>📸 Photo Gallery</h4>
    <div style='display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 15px;'>
        <img src='/images/forklift/page_001_image_01.jpeg' style='width: 100%; border-radius: 8px;' />
        <img src='/images/forklift/page_005_image_01.png' style='width: 100%; border-radius: 8px;' />
        <img src='/images/forklift/page_008_image_09.jpeg' style='width: 100%; border-radius: 8px;' />
    </div>
</div>
```

**Tip:** Add or remove `<img>` tags to change the number of images.

---

## 9. True/False Questions

Simple true or false quiz format.

```html
<div style='margin: 20px 0; padding: 20px; background: #e8f5e9; border-radius: 8px;'>
    <h4 style='color: #2e7d32;'>❓ True or False</h4>
    <div style='margin: 15px 0; padding: 15px; background: white; border-radius: 5px;'>
        <p><strong>You can operate a forklift without a license if supervised.</strong></p>
        <label style='margin-right: 20px;'><input type='radio' name='tf1'> True</label>
        <label><input type='radio' name='tf1'> False ✓</label>
    </div>
    <div style='margin: 15px 0; padding: 15px; background: white; border-radius: 5px;'>
        <p><strong>Maximum speed in a warehouse is 25 km/h.</strong></p>
        <label style='margin-right: 20px;'><input type='radio' name='tf2'> True</label>
        <label><input type='radio' name='tf2'> False ✓</label>
    </div>
</div>
```

---

## 10. Text with Highlights

Emphasize key information with colored highlights.

```html
<div style='margin: 20px 0; padding: 20px; background: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
    <h3 style='color: #00897b;'>📖 Key Information</h3>
    <p>When operating a forklift, always remember:</p>
    <p><span style='background: #ffeb3b; padding: 2px 8px; border-radius: 3px; font-weight: bold;'>Keep the load low</span> to maintain stability.</p>
    <p><span style='background: #ff9800; color: white; padding: 2px 8px; border-radius: 3px; font-weight: bold;'>Never exceed capacity</span> - check the data plate!</p>
    <p><span style='background: #f44336; color: white; padding: 2px 8px; border-radius: 3px; font-weight: bold;'>Always wear PPE</span> when operating.</p>
</div>
```

---

## 11. Information Box (Blue)

Provide additional information or tips.

```html
<div style='margin: 20px 0; padding: 15px; background: #e3f2fd; border-left: 5px solid #2196f3; border-radius: 5px;'>
    <h4 style='color: #1565c0; margin-top: 0;'>ℹ️ Did You Know?</h4>
    <p style='color: #0d47a1; margin-bottom: 0;'>Forklifts were invented in the early 20th century and revolutionized warehouse operations!</p>
</div>
```

---

## 12. Success Box (Green)

Show completed tasks or correct answers.

```html
<div style='margin: 20px 0; padding: 15px; background: #e8f5e9; border-left: 5px solid #4caf50; border-radius: 5px;'>
    <h4 style='color: #2e7d32; margin-top: 0;'>✅ Great Job!</h4>
    <p style='color: #1b5e20; margin-bottom: 0;'>You've completed this section successfully. Move on to the next lesson.</p>
</div>
```

---

## 13. Numbered Steps

Step-by-step instructions.

```html
<div style='margin: 20px 0; padding: 20px; background: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
    <h4 style='color: #00897b;'>📝 Operating Procedure</h4>
    <ol style='line-height: 2;'>
        <li><strong>Pre-start checks:</strong> Inspect the forklift for damage</li>
        <li><strong>Start engine:</strong> Turn key and check all gauges</li>
        <li><strong>Test controls:</strong> Verify brakes, steering, and hydraulics</li>
        <li><strong>Position forks:</strong> Lower forks to ground level</li>
        <li><strong>Approach load:</strong> Drive slowly and align forks</li>
    </ol>
</div>
```

---

## 14. Two-Column Layout

Compare information side-by-side.

```html
<div style='margin: 20px 0; padding: 20px; background: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
    <h4 style='color: #2196f3;'>⚖️ Safe vs Unsafe Practices</h4>
    <div style='display: grid; grid-template-columns: 1fr 1fr; gap: 20px;'>
        <div style='padding: 15px; background: #e8f5e9; border-radius: 5px;'>
            <h5 style='color: #2e7d32;'>✅ SAFE</h5>
            <ul>
                <li>Wear safety vest</li>
                <li>Drive at safe speed</li>
                <li>Check load capacity</li>
                <li>Use horn at corners</li>
            </ul>
        </div>
        <div style='padding: 15px; background: #ffebee; border-radius: 5px;'>
            <h5 style='color: #c62828;'>❌ UNSAFE</h5>
            <ul>
                <li>Operating without training</li>
                <li>Speeding in warehouse</li>
                <li>Overloading forks</li>
                <li>Carrying passengers</li>
            </ul>
        </div>
    </div>
</div>
```

---

## 15. Accordion/Expandable Section

Show/hide detailed information (requires JavaScript).

```html
<div style='margin: 20px 0;'>
    <div style='background: #2196f3; color: white; padding: 15px; border-radius: 8px 8px 0 0; cursor: pointer;' onclick='this.nextElementSibling.style.display = this.nextElementSibling.style.display === "none" ? "block" : "none"'>
        <h4 style='margin: 0;'>▼ Click to expand: Load Capacity Guidelines</h4>
    </div>
    <div style='display: none; padding: 20px; background: white; border: 2px solid #2196f3; border-top: none; border-radius: 0 0 8px 8px;'>
        <p>Load capacity varies by forklift model. Always check the data plate:</p>
        <ul>
            <li>Small forklifts: 1,000 - 2,500 kg</li>
            <li>Medium forklifts: 2,500 - 5,000 kg</li>
            <li>Large forklifts: 5,000+ kg</li>
        </ul>
    </div>
</div>
```

---

## Combining Templates

You can combine multiple templates in one lesson. Example:

```html
<!-- Start with an image -->
<div style='margin: 20px 0; text-align: center;'>
    <img src='/images/forklift/page_001_image_01.jpeg' style='max-width: 100%; border-radius: 8px;' />
</div>

<!-- Add some text -->
<div style='margin: 20px 0; padding: 20px; background: white; border-radius: 8px;'>
    <h3>Forklift Safety Basics</h3>
    <p>Understanding forklift safety is crucial for workplace safety...</p>
</div>

<!-- Add a 3D model -->
<div style='margin: 20px 0; padding: 20px; border: 2px solid #007bff; border-radius: 10px;'>
    <model-viewer src='/models/forklift/FWS_Forklift_01_Blue.gltf' auto-rotate camera-controls style='width: 100%; height: 500px;'></model-viewer>
</div>

<!-- End with a quiz -->
<div style='margin: 20px 0; padding: 20px; background: #fff3cd; border-radius: 8px;'>
    <h4>📋 Knowledge Check</h4>
    <p><strong>What is the maximum safe speed?</strong></p>
    <label style='display: block;'><input type='radio' name='q1'> 8 km/h ✓</label>
    <label style='display: block;'><input type='radio' name='q1'> 25 km/h</label>
</div>
```

---

## Tips & Tricks

### Finding Image Paths
Your images are stored in `/wwwroot/images/forklift/`. Available images include:
- `page_001_image_01.jpeg`
- `page_005_image_01.png`
- `page_008_image_09.jpeg`
- And many more...

Browse your `wwwroot/images/forklift/` folder to see all available images.

### Color Reference
- **Blue:** `#2196f3` (Information)
- **Green:** `#4caf50` (Success)
- **Orange:** `#ff9800` (Warning)
- **Red:** `#f44336` (Danger/Critical)
- **Yellow:** `#ffeb3b` (Highlight)
- **Purple:** `#9c27b0` (Special)
- **Teal:** `#00897b` (Alternative)

### Customization
- Change colors by replacing hex codes (e.g., `#2196f3`)
- Adjust spacing with `margin` and `padding` values
- Modify sizes with `width` and `height` properties
- Update text content to match your lessons

---

## Need Help?

- **Edit lessons:** http://localhost:8080/admin/edit-lesson
- **View 3D demo:** http://localhost:8080/admin/add-3d
- **Your courses:** http://localhost:8080/courses

---

**Created for RTO Web LMS** - Interactive Learning Management System
