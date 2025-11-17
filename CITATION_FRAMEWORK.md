# Government Source Citation Framework
## For RTO Learner Guides and LMS Content

**Document Version:** 1.0
**Created:** 2025-11-17
**Purpose:** Comprehensive framework for adding government source citations to RTO training materials

---

## 1. PRIMARY GOVERNMENT SOURCES

### 1.1 Training Package Authority

**training.gov.au - National Register of VET**
- **Website:** https://training.gov.au
- **Authority:** Australian Government Department of Employment and Workplace Relations
- **Purpose:** Official source for all nationally recognised training packages, units of competency, and qualifications
- **Usage:** Primary source for unit requirements, performance criteria, assessment conditions, and foundation skills
- **Non-copyright Status:** Commonwealth Government public information
- **Citation Format:**
  ```
  Australian Government. (Year). [Unit Code] [Unit Title]. training.gov.au.
  Retrieved [Date], from https://training.gov.au/Training/Details/[unitcode]
  ```

**Example Units in Scope:**
- TLILIC0003 - Licence to operate a forklift truck
- TLILIC0005 - Operate a boom-type elevating work platform (boom length 11 metres or more)
- TLILIC0006 - Licence to operate a bridge and gantry crane
- TLILIC0009 - Licence to operate a vehicle loading crane (capacity 10 metre tonnes and above)
- TLILIC0020 - Licence to operate a slewing mobile crane (up to 20 tonnes)
- TLILIC0021 - Licence to operate a slewing mobile crane (up to 100 tonnes)
- TLILIC0023 - Licence to operate a slewing mobile crane (up to 60 tonnes)
- CPCCLRG3001 - Licence to perform rigging basic level
- CPCCLRG4001 - Licence to perform rigging advanced level
- RIIHAN305D - Operate a gantry or overhead crane
- RIIMPO336E - Conduct rigid haul truck operations
- RIIMPO337E - Conduct articulated dump truck operations
- RIIMPO338E - Conduct articulated haul truck operations
- RIIPRO301D - Conduct crushing and screening plant operations
- RIIWHS204E - Work safely at heights
- RIISS00054 - Traffic Controller Skill Set
- MSMWHS217 - Gas test atmospheres
- HLTAID011 - Provide first aid

### 1.2 Workplace Health and Safety Authority

**Safe Work Australia**
- **Website:** https://www.safeworkaustralia.gov.au
- **Authority:** Australian Government statutory agency
- **Purpose:** Develops model WHS laws, codes of practice, and guidance materials
- **Key Resources:**
  - Model Work Health and Safety Regulations (consolidated, updated regularly)
  - Model Codes of Practice
  - Guidance Materials
  - General Guides
- **Non-copyright Status:** Creative Commons Attribution 4.0 International licence (where specified)
- **Citation Format:**
  ```
  Safe Work Australia. (Year). [Document Title]. Retrieved [Date],
  from https://www.safeworkaustralia.gov.au/[path]
  ```

**Key Documents:**
- **General guide for industrial lift trucks** (March 2020)
  - URL: https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks
  - Topics: Risk management, pre-start checks, safe operation, load handling, maintenance

- **Model Code of Practice: Managing the risks of plant in the workplace** (March 2016)
  - URL: https://www.safeworkaustralia.gov.au/doc/model-code-practice-managing-risks-plant-workplace
  - Topics: Plant safety, maintenance, high-risk plant

- **Model Code of Practice: How to manage work health and safety risks**
  - URL: https://www.safeworkaustralia.gov.au/doc/model-code-practice-how-manage-work-health-and-safety-risks
  - Topics: Risk management framework, hierarchy of controls

- **Model WHS Regulations** (Updated 1 September 2024)
  - URL: https://www.safeworkaustralia.gov.au/law-and-regulation/model-whs-laws
  - Topics: High risk work licensing (Part 4.5), plant safety, specific requirements

- **High risk work licences**
  - URL: https://www.safeworkaustralia.gov.au/safety-topic/managing-health-and-safety/licences
  - Topics: HRWL requirements, classes, national recognition

### 1.3 State and Territory Work Safety Regulators

**SafeWork NSW**
- **Website:** https://www.safework.nsw.gov.au
- **Authority:** NSW Government work health and safety regulator
- **Key Topics:** High risk work licences, forklift safety, workplace safety
- **Key Resources:**
  - High risk work licences: https://www.safework.nsw.gov.au/licences-and-registrations/licences/high-risk-work-licences
  - Forklifts guidance: https://www.safework.nsw.gov.au/hazards-a-z/forklifts
- **Citation Format:**
  ```
  SafeWork NSW. (Year). [Document Title]. Retrieved [Date],
  from https://www.safework.nsw.gov.au/[path]
  ```

**WorkSafe Victoria**
- **Website:** https://www.worksafe.vic.gov.au
- **Authority:** Victorian Government work health and safety regulator
- **Key Resources:**
  - High risk work licences: https://www.worksafe.vic.gov.au/high-risk-work-licence
  - Forklift hazards and controls: https://www.worksafe.vic.gov.au/forklift-hazards-and-risk-controls
- **Citation Format:**
  ```
  WorkSafe Victoria. (Year). [Document Title]. Retrieved [Date],
  from https://www.worksafe.vic.gov.au/[path]
  ```

**Other State/Territory Regulators:**
- WorkSafe WA: https://www.worksafe.wa.gov.au
- WorkSafe Queensland: https://www.worksafe.qld.gov.au
- SafeWork SA: https://www.safework.sa.gov.au
- WorkSafe Tasmania: https://worksafe.tas.gov.au
- NT WorkSafe: https://worksafe.nt.gov.au
- WorkSafe ACT: https://www.worksafe.act.gov.au

---

## 2. CITATION TEMPLATE FOR LESSON CONTENT

### 2.1 Standard Citation Block

Add this at the end of each lesson or section:

```html
<div style='margin: 30px 0; padding: 20px; background: #f8f9fa; border-left: 4px solid #0066cc; border-radius: 5px;'>
    <h4 style='color: #0066cc; margin-top: 0;'>📚 Official Sources & References</h4>
    <p style='font-size: 0.95em; line-height: 1.6; margin-bottom: 10px;'>
        This training content is based on official Australian Government sources and nationally
        recognised training requirements:
    </p>
    <ul style='font-size: 0.9em; line-height: 1.8;'>
        <li><strong>Unit of Competency:</strong> Australian Government. (Year). [Unit Code] [Unit Title].
            training.gov.au. Retrieved [Date], from
            <a href='https://training.gov.au/Training/Details/[unitcode]' target='_blank'>
                https://training.gov.au/Training/Details/[unitcode]
            </a>
        </li>
        <li><strong>Safety Guidelines:</strong> Safe Work Australia. ([Year]). [Guide Title].
            Retrieved [Date], from
            <a href='[URL]' target='_blank'>[URL]</a>
        </li>
        <li><strong>Regulatory Requirements:</strong> [State Regulator]. ([Year]). [Document Title].
            Retrieved [Date], from
            <a href='[URL]' target='_blank'>[URL]</a>
        </li>
    </ul>
    <p style='font-size: 0.85em; color: #666; margin-bottom: 0; margin-top: 15px;'>
        <em>Note: All learners must verify current requirements with their state or territory
        work safety regulator before commencing high risk work.</em>
    </p>
</div>
```

### 2.2 Inline Citation Format

For specific content within lessons, use inline citations:

```html
<p>
    According to Safe Work Australia's General guide for industrial lift trucks
    <a href='https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks'
       target='_blank' style='color: #0066cc;'>[1]</a>,
    operators must conduct pre-start checks at the beginning of each shift.
</p>
```

### 2.3 Section-Specific Citations

For detailed technical sections, add specific references:

```html
<div style='margin: 15px 0; padding: 12px; background: #e3f2fd; border-left: 3px solid #2196f3; font-size: 0.9em;'>
    <strong>Source:</strong> TLILIC0003 Element 3 - Performance Criteria 3.2: "Load is moved according
    to site and organisational procedures and the manufacturer's specifications"
    <br>
    <a href='https://training.gov.au/Training/Details/tlilic0003' target='_blank'
       style='color: #1976d2;'>training.gov.au/TLILIC0003</a>
</div>
```

---

## 3. LESSON-TO-SOURCE MAPPING

### Module 1: Introduction and Planning

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 1 | What Is A Forklift? | TLILIC0003, SWA General Guide |
| 2 | Parts Of A Forklift | TLILIC0003, Manufacturer specs (generic) |
| 3 | Forklift Attachments | TLILIC0003, SWA General Guide |
| 4 | Plan Work Activities | TLILIC0003 Element 1, SWA Risk Management COP |
| 5 | Workplace Procedures | TLILIC0003 Element 1, Model WHS Regulations |

### Module 2: Risk Management

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 6 | Identify Hazards | SWA Risk Management COP, TLILIC0003 Element 1 |
| 7 | Assess Risks | SWA Risk Management COP, Model WHS Regulations |
| 8 | Control Risks | SWA Risk Management COP (Hierarchy of Controls) |

### Module 3: Forklift Trucks

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 9 | Types Of Forklifts | SWA General Guide, TLILIC0003 |
| 10 | Forklift Components | SWA General Guide, TLILIC0003 |
| 11 | Forklift Controls | SWA General Guide, TLILIC0003 Element 2 |

### Module 4: Pre-Start Checks

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 12 | Pre-Operational Checks | TLILIC0003 Element 2, SWA General Guide Section 4 |
| 13 | Documentation | TLILIC0003 Element 1 & 5, Model WHS Regulations |

### Module 5: Operating a Forklift

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 14 | Basic Operations | TLILIC0003 Element 2, SWA General Guide Section 5 |

### Module 6: Shift a Load

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 15 | Load Assessment | TLILIC0003 Element 3, SWA General Guide Section 6 |
| 16 | Picking Up Loads | TLILIC0003 Element 3, SWA General Guide Section 6 |
| 17 | Traveling With Loads | TLILIC0003 Element 3, SWA General Guide Section 7 |
| 18 | Placing Loads | TLILIC0003 Element 3, SWA General Guide Section 6 |
| 19 | Stacking and Destacking | TLILIC0003 Element 3, SWA General Guide |

### Module 7: Emergency Procedures

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 20 | Emergency Response | TLILIC0003 Element 4, Model WHS Regulations Part 3.2 |
| 21 | Incident Reporting | TLILIC0003 Element 5, Model WHS Regulations Part 3.3 |

### Module 8: Post-Operational Procedures

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 22 | Shutting Down | TLILIC0003 Element 2, SWA General Guide Section 8 |
| 23 | Post-Operation Checks | TLILIC0003 Element 5, SWA General Guide |

### Module 9: Maintenance

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 24 | Basic Maintenance | TLILIC0003 Element 5, SWA Plant COP, Model WHS Regs |
| 25 | Fault Reporting | TLILIC0003 Element 5, SWA Plant COP |

### Module 10: Communication

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 26 | Workplace Communication | TLILIC0003 Element 1 & 4, Model WHS Regulations |
| 27 | Hand Signals | TLILIC0003, Industry standard practices |

### Module 11: Working at Heights

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 28 | Elevated Work Platforms | TLILIC0003 Element 3, SWA Work at Heights COP |

### Module 12: Load Charts and Capacity

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 29 | Understanding Load Charts | TLILIC0003 Element 3, SWA General Guide Section 6 |
| 30 | Load Capacity Calculations | TLILIC0003 Element 3, SWA General Guide Section 6 |

### Module 13: Summary and Assessment

| Lesson | Title | Primary Sources |
|--------|-------|----------------|
| 31 | Course Summary | TLILIC0003 All Elements |
| 32 | Final Assessment | TLILIC0003 Assessment Requirements |

---

## 4. SPECIFIC CONTENT REFERENCES

### 4.1 High Risk Work Licensing

**Content Topic:** HRWL requirements and application process

**Sources:**
1. **Model WHS Regulations Part 4.5** - High risk work
   - URL: https://www.safeworkaustralia.gov.au/law-and-regulation/model-whs-laws
   - Sections: Regulations 80-92
   - Topics: HRWL requirements, competency, exemptions

2. **Safe Work Australia - High risk work licences**
   - URL: https://www.safeworkaustralia.gov.au/safety-topic/managing-health-and-safety/licences
   - Topics: Licence classes, national recognition, requirements

3. **State/Territory Application Process** (use relevant state):
   - SafeWork NSW: https://www.safework.nsw.gov.au/licences-and-registrations/licences/high-risk-work-licences
   - WorkSafe Victoria: https://www.worksafe.vic.gov.au/high-risk-work-licence

**Citation Example:**
```
Under Part 4.5 of the Model Work Health and Safety Regulations, a person must hold a
high risk work licence to operate a forklift truck [1]. Competency in the TLILIC0003
unit alone does not constitute a licence; learners must apply separately to their
state or territory regulator [2].

[1] Safe Work Australia. (2024). Model Work Health and Safety Regulations.
    Retrieved November 2025, from https://www.safeworkaustralia.gov.au/law-and-regulation/model-whs-laws
[2] Australian Government. (2025). TLILIC0003 Licence to operate a forklift truck.
    training.gov.au. Retrieved November 2025, from https://training.gov.au/Training/Details/tlilic0003
```

### 4.2 Pre-Start Checks

**Content Topic:** Daily pre-operational inspection requirements

**Sources:**
1. **TLILIC0003 Element 2** - Performance Criteria 2.1-2.3
   - Pre-start inspection procedures
   - Identification of faults
   - Reporting mechanisms

2. **SWA General Guide for Industrial Lift Trucks** - Section 4: Before you start work
   - URL: https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks
   - Topics: Daily checks, documentation, fault reporting

**Citation Example:**
```
Operators must conduct pre-start safety checks at the beginning of each shift, as the
forklift may not have been left in a safe condition by the previous operator [1].
TLILIC0003 Element 2 requires learners to demonstrate competency in conducting pre-operational
inspections and identifying equipment faults [2].

[1] Safe Work Australia. (2020). General guide for industrial lift trucks (Section 4).
    Retrieved November 2025, from https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks
[2] Australian Government. (2025). TLILIC0003 Element 2: Conduct pre-operational checks.
    training.gov.au. Retrieved November 2025, from https://training.gov.au/Training/Details/tlilic0003
```

### 4.3 Risk Management Process

**Content Topic:** Identify, assess, control hazards

**Sources:**
1. **Model Code of Practice: How to manage work health and safety risks**
   - URL: https://www.safeworkaustralia.gov.au/doc/model-code-practice-how-manage-work-health-and-safety-risks
   - Topics: Risk management process, hierarchy of controls

2. **TLILIC0003 Element 1** - Prepare to operate forklift
   - Performance Criteria 1.1-1.5
   - Workplace hazards and risks

**Citation Example:**
```
The risk management process involves identifying hazards, assessing risks, implementing
controls, and reviewing effectiveness [1]. When operating forklifts, operators must
identify workplace hazards and implement risk controls according to site procedures and
the hierarchy of controls [2].

[1] Safe Work Australia. (2018). Model Code of Practice: How to manage work health and
    safety risks. Retrieved November 2025, from
    https://www.safeworkaustralia.gov.au/doc/model-code-practice-how-manage-work-health-and-safety-risks
[2] Australian Government. (2025). TLILIC0003 Element 1: Prepare to operate forklift.
    training.gov.au. Retrieved November 2025, from https://training.gov.au/Training/Details/tlilic0003
```

### 4.4 Load Capacity and Stability

**Content Topic:** Load charts, capacity plates, stability principles

**Sources:**
1. **TLILIC0003 Element 3** - Performance Criteria 3.1-3.5
   - Load assessment
   - Load capacity verification
   - Safe load handling

2. **SWA General Guide for Industrial Lift Trucks** - Section 6: Loads
   - URL: https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks
   - Topics: Load capacity, stability triangle, center of gravity

**Citation Example:**
```
The load capacity plate (data plate) provides essential information about the forklift's
maximum load capacity at specified load centers [1]. Operators must verify that loads
do not exceed the forklift's rated capacity and understand stability principles including
the stability triangle and center of gravity [2].

[1] Australian Government. (2025). TLILIC0003 Element 3: Shift a load. training.gov.au.
    Retrieved November 2025, from https://training.gov.au/Training/Details/tlilic0003
[2] Safe Work Australia. (2020). General guide for industrial lift trucks (Section 6: Loads).
    Retrieved November 2025, from https://www.safeworkaustralia.gov.au/doc/general-guide-industrial-lift-trucks
```

---

## 5. ADDITIONAL UNITS CITATION GUIDES

### 5.1 Crane Operations (TLILIC series)

**TLILIC0020, TLILIC0021, TLILIC0023** - Slewing mobile cranes

**Primary Sources:**
- training.gov.au unit pages for each specific crane class
- Safe Work Australia - Model Code of Practice: Managing risks of plant in the workplace
- State/territory crane licensing authorities

**Key Topics to Cite:**
- Load charts and rated capacity
- Crane setup and stability
- Lift planning and risk assessment
- Communication and signaling
- Emergency procedures

### 5.2 Rigging (CPCCLRG series)

**CPCCLRG3001** - Rigging basic level
**CPCCLRG4001** - Rigging advanced level

**Primary Sources:**
- training.gov.au unit pages
- Safe Work Australia - Model Code of Practice: Managing risks of plant in the workplace
- AS 1418 series (referenced, not quoted - Australian Standards are copyright)

**Key Topics to Cite:**
- Rigging hardware inspection and selection
- Load assessment and weight estimation
- Slinging techniques
- Communication with crane operators

### 5.3 Working at Heights (RIIWHS204E)

**Primary Sources:**
- training.gov.au RIIWHS204E page
- Safe Work Australia - Model Code of Practice: Managing the risk of falls at workplaces
- Model WHS Regulations Part 4.4

**Key Topics to Cite:**
- Fall prevention hierarchy
- Fall arrest systems
- Harness inspection and use
- Rescue procedures

### 5.4 First Aid (HLTAID011)

**Primary Sources:**
- training.gov.au HLTAID011 page
- Australian Resuscitation Council guidelines
- Safe Work Australia - Model Code of Practice: First aid in the workplace

**Key Topics to Cite:**
- CPR procedures (ARC guidelines)
- Emergency response
- Incident documentation
- Workplace first aid requirements

---

## 6. CITATION PLACEMENT GUIDELINES

### 6.1 Where to Place Citations

1. **End of each lesson** - Comprehensive citation block listing all sources used
2. **After regulatory statements** - Inline citations for specific requirements
3. **In warning/safety boxes** - Citations for safety-critical information
4. **After technical specifications** - Citations for capacity, specifications, procedures

### 6.2 What Content Requires Citations

**MUST be cited:**
- Regulatory requirements (e.g., "must hold a HRWL")
- Performance criteria from training packages
- Safety procedures and guidelines
- Risk management processes
- Emergency procedures
- Incident reporting requirements
- Licensing and legal requirements

**DOES NOT require citation:**
- General workplace practices
- Basic definitions (unless from official source)
- Common sense safety (use good judgement)
- Manufacturer-specific information (cite manufacturer instead)

### 6.3 Citation Currency

- **Check dates:** Verify all URLs and documents are current as of 2025
- **Update cycle:** Review citations annually or when regulations change
- **Version tracking:** Note document versions where applicable
- **Archived content:** If content is superseded, note replacement document

---

## 7. QUALITY ASSURANCE CHECKLIST

### Before Publishing Content:

- [ ] Every regulatory statement has a citation
- [ ] All training package requirements reference training.gov.au
- [ ] Safety procedures cite Safe Work Australia or state regulator
- [ ] URLs are tested and functional
- [ ] Retrieval dates are included
- [ ] Citations follow consistent format
- [ ] No RTO names are mentioned (as per requirement)
- [ ] All sources are government/non-copyright
- [ ] State-specific content identifies relevant state
- [ ] Documents versions are noted where applicable

---

## 8. DOCUMENT MAINTENANCE

**Review Schedule:** Quarterly or when legislation changes
**Responsible:** Content/Compliance Team
**Version Control:** Update version number and date when framework changes

**Key Monitoring:**
- training.gov.au for unit updates/supersessions
- Safe Work Australia for new/updated codes and guides
- State regulators for licensing procedure changes
- Model WHS Regulations for legislative amendments

---

## 9. CONTACT INFORMATION

**For Questions or Issues:**

**training.gov.au Support:**
- Website: https://training.gov.au/Home/Tga
- Email: training@dese.gov.au

**Safe Work Australia:**
- Website: https://www.safeworkaustralia.gov.au
- Email: info@swa.gov.au

**State/Territory Regulators:** See Section 1.3 for contact details

---

**Document Control:**
- **Version:** 1.0
- **Date:** 2025-11-17
- **Next Review:** 2026-02-17
- **Author:** RTO Content Development Team
- **Approved by:** [Pending]

---

*This framework ensures all training content for the AI tutor system is properly attributed to official government sources, maintaining RTO compliance and providing learners with traceable, verifiable information.*
